﻿using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Slim_Student.Model;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    /* 알고리즘
     * 
     * 1. 유저(학생) 정보 가져오기
     * 2. 해당 학생이 듣는 교과목 정보 가져오기 (sub_ids속성을 참고)
     * 3. 교과목 정보마다 선생님의 lectureler_id를 이용하여 다시 user테이블을 참고하여 선생님 이름(user_name) 가져오기
     * */
    class ViewModelSubjectList : ViewModelBase
    {
        private DBManager dbManager;
        private DB_Subject dbSubject;
        private SubjectList parentWindow;
        private MainFrame parentMf;

        private const int FINISH_CLASS = 0;

        // 생성자
        public ViewModelSubjectList(SubjectList pWindow)
        {
            dbManager = new DBManager();
            dbSubject = new DB_Subject(dbManager);
            parentWindow = pWindow;
            _ItemList = new List<object[]>();
        }

        public ViewModelSubjectList(SubjectList pWindow, MainFrame mf)
        {
            dbManager = new DBManager();
            dbSubject = new DB_Subject(dbManager);
            parentWindow = pWindow;
            _ItemList = new List<object[]>();
            parentMf = mf;
        }

        #region SubjectItemList
        private List<SubjectInfo> _SubjectItemList;
        public List<SubjectInfo> SubjectItemList
        {
            get { return _SubjectItemList; }
            set
            {
                _SubjectItemList = value;
            }
        }
        #endregion

        #region ItemList
        private List<object[]> _ItemList;
        public List<object[]> ItemList
        {
            get { return _ItemList; }
            set { _ItemList = value; }
        }
        #endregion

        #region MinimizeCommand
        private ICommand _MinimizeCommand;
        public ICommand MinimizeCommand
        {
            get
            {
                return _MinimizeCommand ?? (_MinimizeCommand = new AppCommand(MinimizeCommandFunc));
            }
        }
        public void MinimizeCommandFunc(Object o)
        {
            Widget widget = new Widget();
            widget.Show();
            MainFrame.Frame.Hide();
        }
        #endregion

        #region EnterSubjectCommand
        private ICommand _EnterSubjectCommand;
        public ICommand EnterSubjectCommand
        {
            get
            {
                return _EnterSubjectCommand ?? (_EnterSubjectCommand = new AppCommand(EnterSubjectCommandFunc));
            }
        }

        private void EnterSubjectCommandFunc(Object o)
        {
            int idx = parentWindow.SubjectListBox.SelectedIndex;
            if (idx < 0)
                return;
            int canLogin = dbSubject.SelectIsProcessing(SubjectItemList.ElementAt(idx).SubjectId); // 선택한 과목의 subid를 전달해서 강사가 접속상태인지 확인
            if (canLogin == FINISH_CLASS)
            {
                MessageBox.Show("아직 접속할 수 없습니다.");
                return;
            }
            MessageBox.Show("출석 되었습니다.");
            parentWindow.NavigationService.Navigate(new PageMainSubject(ItemList[idx], parentWindow));
        }
        #endregion


        #region LogoutCommand
        private ICommand _LogoutCommand;
        public ICommand LogoutCommand
        {
            get { return _LogoutCommand ?? (_LogoutCommand = new AppCommand(LogoutCommandFunc)); }
        }

        public void LogoutCommandFunc(object o)
        {
            
            LoginWindow loginwindow = new LoginWindow();
            loginwindow.Show();
            MainFrame.closeWindow();
            
        }
        #endregion


        /* makeList(void) 메소드
         * 기능: sub_ids.Length개 만큼의 과목 리스트를 만든다.         
         */
        public void makeList()
        {
            object[] items = MainFrame.UserInfo;    // 1. 유저(학생) 정보 가져오기
            // _기준으로 배열에 string넣음
            string[] sub_ids = items[(int)DB_User.FIELD.sub_ids].ToString().Split('_'); 
            for (int k = 0; k < sub_ids.Length; k++)
                // 2. 해당 학생이 듣는 교과목 정보 가져오기 (sub_ids속성을 참고)
                ItemList.Add(dbSubject.SelectSubjectListForStudent(Convert.ToInt32(sub_ids[k])));

            SubjectItemList = SubjectInfo.Data(ItemList);
        }

        internal class SubjectInfo   
        {
            private static List<SubjectInfo> data;

            public int SubjectId { get; private set; }          
            public string SubjectName { get; private set; }
            public string LecTimeLocation { get; private set; }
            public string LecturelerName { get; private set; }
            public string SubjectTime { get; private set; }
            public string SubjectLocation { get; private set; }
            
            public static List<SubjectInfo> Data(List<object[]> items)
            {
                SubjectInfo subjectTemp;
                data = new List<SubjectInfo>();

                for(int i=0;i<items.Count;i++){
                    string lecturelerNameTemp = GetLecturerName(Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.lectureler_id)));
                    string subjectTimeTemp = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.time));
                    string subjectLocationTemp = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.location));
                    subjectTemp = new SubjectInfo
                    {
                        SubjectId = Convert.ToInt32(items[i].ElementAt((int)DB_Subject.FIELD.sub_id)),
                        SubjectName = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.sub_name)),
                        LecturelerName = lecturelerNameTemp,
                        SubjectTime = subjectTimeTemp,
                        SubjectLocation = subjectLocationTemp,
                        LecTimeLocation = "[" + lecturelerNameTemp + "] "
                                        + "[" + subjectTimeTemp + "] "
                                        + "[" + subjectLocationTemp + "]"                   
                    };
                    data.Add(subjectTemp);
                }
                return data;
            }

            private static string GetLecturerName(string user_id) // 3. 교과목 정보마다 선생님의 lectureler_id를 이용하여 다시 user테이블을 참고하여 선생님 이름(user_name) 가져오기
            {
                DBManager dbm = new DBManager();
                DB_User dbUser = new DB_User(dbm);
                return Convert.ToString(dbUser.SelectUser(user_id)[(int)DB_User.FIELD.user_name]);
            }
        }

        /*
         * User Profile에 필요한 프로퍼티(Group과 이름)
         */
        #region Profile
        public string UserGroup
        {
            get { return (string)MainFrame.UserInfo[(int)DB_User.FIELD.group]; }
        }

        public string UserName
        {
            get { return (string)MainFrame.UserInfo[(int)DB_User.FIELD.user_name]; }
        }
        #endregion
    }
}
