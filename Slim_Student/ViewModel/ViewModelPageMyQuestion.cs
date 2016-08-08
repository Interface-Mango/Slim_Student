using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Slim_Student.Model;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelPageMyQuestion : ViewModelBase
    {
        private DBManager dbManager;
        private DB_MyQuestion dbMyQustion;
        private PageMyQuestion parentWindow;

        // 생성자
        public ViewModelPageMyQuestion(PageMyQuestion pWindow)
        {
            dbManager = new DBManager();
            dbMyQustion = new DB_MyQuestion(dbManager);
            parentWindow = pWindow;
            _ItemList = new List<object[]>();
            //parentWindow.QuestionListBox.MouseDoubleClick += new MouseButtonEventHandler(QuestionListBox_MouseDoubleClick);
        }

        #region QuestionItemList
        private List<QuestionInfo> _QuestionItemList;
        public List<QuestionInfo> QuestionItemList
        {
            get { return _QuestionItemList; }
            set
            {
                _QuestionItemList = value;
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

        #region WriteQuestionCommand
        private ICommand _WriteQuestionCommand;
        public ICommand WriteQuestionCommand
        {
            get { return _WriteQuestionCommand ?? (_WriteQuestionCommand = new AppCommand(WriteQuestionCommandFunc)); }
        }

        public void WriteQuestionCommandFunc(Object o)
        {
            //TODO: 글 작성 다이얼로그 띄우기
        }
        #endregion
        
        #region ListBoxItem_MouseDoubleClick
        private ICommand _ListBoxItem_MouseDoubleClickCommand;
        public ICommand ListBoxItem_MouseDoubleClickCommand
        {
            get { return _ListBoxItem_MouseDoubleClickCommand ?? (_ListBoxItem_MouseDoubleClickCommand = new AppCommand(MyQuestionListBoxItem_MouseDoubleClick)); }
        }
        public void MyQuestionListBoxItem_MouseDoubleClick(Object o)
        {
            MessageBox.Show("################");
        }

        private void QuestionListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }
        #endregion

        public void makeList()
        {
            object[] userItems = MainFrame.UserInfo;    // 1. 유저(학생) 정보 가져오기
            object[] subItems = PageMainSubject.SubjectInfo;
            // _기준으로 배열에 string넣음
            string std_id = Convert.ToString(userItems[(int)DB_User.FIELD.user_id]);
            int sub_id = Convert.ToInt32(subItems[(int)DB_Subject.FIELD.sub_id]);

            ItemList = dbMyQustion.SelectMyQuestionList(Convert.ToInt32(std_id), Convert.ToInt32(sub_id));
            if (ItemList != null)
                QuestionItemList = QuestionInfo.Data(ItemList);
        }

        internal class QuestionInfo
        {
            private static List<QuestionInfo> data;

            public int MyQuestionId { get; private set; }
            public string MyQuestionStudentId { get; private set; }
            public int MyQuestionSubjectId { get; private set; }
            public string MyQuestionContent { get; private set; }

            public static List<QuestionInfo> Data(List<object[]> items)
            {
                QuestionInfo subjectTemp;
                data = new List<QuestionInfo>();

                for (int i = 0; i < items.Count; i++)
                {
                    string myQuestionContent = Convert.ToString(items[i].ElementAt((int)DB_MyQuestion.FIELD.content));
                    subjectTemp = new QuestionInfo
                    {
                        MyQuestionId = (i+1),
                        MyQuestionStudentId = Convert.ToString(items[i].ElementAt((int)DB_MyQuestion.FIELD.std_id)),
                        MyQuestionSubjectId = Convert.ToInt32(items[i].ElementAt((int)DB_MyQuestion.FIELD.sub_id)),
                        MyQuestionContent = Convert.ToString(items[i].ElementAt((int)DB_MyQuestion.FIELD.content))
                    };
                    data.Add(subjectTemp);
                }
                return data;
            }
            /*
            private static string GetSubjectName(string sub_id) 
            {
                DBManager dbm = new DBManager();
                DB_User dbUser = new DB_User(dbm);
                return Convert.ToString(dbUser.SelectUser(user_id)[(int)DB_User.FIELD.user_name]);
            }*/
        }
    }
}
