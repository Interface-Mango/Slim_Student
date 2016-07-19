using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Slim_Student.Model;

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

        // 생성자
        public ViewModelSubjectList(SubjectList pWindow)
        {
            dbManager = new DBManager();
            dbSubject = new DB_Subject(dbManager);
            parentWindow = pWindow;
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

        /* makeList(void) 메소드
         * 기능: sub_ids.Length개 만큼의 과목 리스트를 만든다.         
         */
        public void makeList()
        {
            object[] items = MainFrame.UserInfo;    // 1. 유저(학생) 정보 가져오기
            string[] sub_ids = items[(int)DB_User.FIELD.sub_ids].ToString().Split('_');
            List<object[]> subjectItems = new List<object[]>();
            for (int k = 0; k < sub_ids.Length; k++)            
                subjectItems.Add(dbSubject.SelectSubjectListForStudent(Convert.ToInt32(sub_ids[k]))); // 2. 해당 학생이 듣는 교과목 정보 가져오기 (sub_ids속성을 참고)
            SubjectInfo sInfo = new SubjectInfo(parentWindow);
            SubjectItemList = sInfo.Data(subjectItems);
        }

        internal class SubjectInfo
        {
            private SubjectList parentWindow;

            public int SubjectId { get; private set; }
            public string SubjectName { get; private set; }
            public string LecTimeLocation { get; private set; }
            public string LecturelerName { get; private set; }
            public string SubjectTime { get; private set; }
            public string SubjectLocation { get; private set; }

            public ICommand EnterSubjectCommand { get; private set; }

            private void EnterSubjectCommandFunc(Object o)
            {
                object[] properties = { SubjectId, SubjectName, LecTimeLocation, LecturelerName, SubjectTime, SubjectLocation };
                parentWindow.NavigationService.Navigate(new PageMainSubject(properties));
            }

            public SubjectInfo(SubjectList pWindow)
            {
                parentWindow = pWindow;
            }

            public List<SubjectInfo> Data(List<object[]> items)
            {
                SubjectInfo subjectTemp;
                var data = new List<SubjectInfo>();

                for(int i=0;i<items.Count;i++){
                    string lecturelerNameTemp = GetLecturelerName(Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.lectureler_id)));
                    string subjectTimeTemp = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.time));
                    string subjectLocationTemp = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.location));
                    subjectTemp = new SubjectInfo(parentWindow)
                    {
                        SubjectId = Convert.ToInt32(items[i].ElementAt((int)DB_Subject.FIELD.sub_id)),
                        SubjectName = Convert.ToString(items[i].ElementAt((int)DB_Subject.FIELD.sub_name)),
                        LecturelerName = lecturelerNameTemp,
                        SubjectTime = subjectTimeTemp,
                        SubjectLocation = subjectLocationTemp,
                        LecTimeLocation = "[" + lecturelerNameTemp + "] "
                                        + "[" + subjectTimeTemp + "] "
                                        + "[" + subjectLocationTemp + "]",
                        EnterSubjectCommand = new AppCommand(EnterSubjectCommandFunc)                        
                    };
                    data.Add(subjectTemp);
                }
                return data;
            }

            private string GetLecturelerName(string user_id) // 3. 교과목 정보마다 선생님의 lectureler_id를 이용하여 다시 user테이블을 참고하여 선생님 이름(user_name) 가져오기
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

        #region 
        #endregion
    }
}
