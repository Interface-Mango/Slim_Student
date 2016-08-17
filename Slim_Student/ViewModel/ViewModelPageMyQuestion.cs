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
        private DB_MyQuestion dbMyQuestion;
        private PageMyQuestion parentWindow;

        // 생성자
        public ViewModelPageMyQuestion(PageMyQuestion pWindow)
        {
            dbManager = new DBManager();
            dbMyQuestion = new DB_MyQuestion(dbManager);
            parentWindow = pWindow;
            _ItemList = new List<object[]>();
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
            PageWriteNote.isInsertUpdate = true;

            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageWriteNote.xaml", UriKind.Relative);
        }
        #endregion

        public void makeList()
        {
            object[] userItems = MainFrame.UserInfo;    // 1. 유저(학생) 정보 가져오기
            object[] subItems = PageMainSubject.SubjectInfo;
            // _기준으로 배열에 string넣음
            string std_id = Convert.ToString(userItems[(int)DB_User.FIELD.user_id]);
            int sub_id = Convert.ToInt32(subItems[(int)DB_Subject.FIELD.sub_id]);

            ItemList = dbMyQuestion.SelectMyQuestionList(std_id, Convert.ToInt32(sub_id));
            if (ItemList != null)
                QuestionItemList = QuestionInfo.Data(ItemList, dbMyQuestion);
        }

        internal class QuestionInfo
        {
            private static List<QuestionInfo> data;
            private static DB_MyQuestion dbMyQuestion;

            public int Id { get; private set; }
            public int MyQuestionId { get; private set; }
            public string MyQuestionStudentId { get; private set; }
            public int MyQuestionSubjectId { get; private set; }
            private string originContent;
            private string _MyQuestionContent;
            public string MyQuestionContent { 
                get
                {
                    return _MyQuestionContent;
                }
                set
                {
                    originContent = value;
                    if (value.Length > 100) {
                        string temp = "";
                        for (int i = 0; i < 100;i++ )
                        {
                            temp += value[i];
                        }
                        temp += "...";
                        _MyQuestionContent = temp;
                    } else {
                        _MyQuestionContent = value;
                    }
                } 
            }
            public DateTime MyQuestionDate { get; private set; }

            public static List<QuestionInfo> Data(List<object[]> items, DB_MyQuestion _dbMyQuestion)
            {
                QuestionInfo subjectTemp;
                data = new List<QuestionInfo>();
                dbMyQuestion = _dbMyQuestion;

                for (int i = 0; i < items.Count; i++)
                {
                    subjectTemp = new QuestionInfo
                    {
                        Id = items.Count-i,
                        MyQuestionId = Convert.ToInt32(items[i].ElementAt((int)DB_MyQuestion.FIELD.id)),
                        MyQuestionStudentId = Convert.ToString(items[i].ElementAt((int)DB_MyQuestion.FIELD.std_id)),
                        MyQuestionSubjectId = Convert.ToInt32(items[i].ElementAt((int)DB_MyQuestion.FIELD.sub_id)),
                        MyQuestionContent = Convert.ToString(items[i].ElementAt((int)DB_MyQuestion.FIELD.content)),
                        MyQuestionDate = Convert.ToDateTime(items[i].ElementAt((int)DB_MyQuestion.FIELD.date))
                    };
                    data.Add(subjectTemp);
                }
                return data;
            }

            #region ListBoxItem_MouseDoubleClick
            private ICommand _ListBoxItem_MouseDoubleClickCommand;
            public ICommand ListBoxItem_MouseDoubleClickCommand
            {
                get { return _ListBoxItem_MouseDoubleClickCommand ?? (_ListBoxItem_MouseDoubleClickCommand = new AppCommand(MyQuestionListBoxItem_MouseDoubleClick)); }
            }
            public void MyQuestionListBoxItem_MouseDoubleClick(Object o)
            {
                PageMyQuestionDetail.mId = MyQuestionId;
                PageMyQuestionDetail.mStdId = MyQuestionStudentId;
                PageMyQuestionDetail.mSubId = MyQuestionSubjectId;
                PageMyQuestionDetail.mContent = originContent;
                PageMyQuestionDetail.mDate = MyQuestionDate;

                ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageMyQuestionDetail.xaml", UriKind.Relative);
            }
            #endregion

            #region QuestionModifyCommand
            private ICommand _QuestionModifyCommand;
            public ICommand QuestionModifyCommand
            {
                get { return _QuestionModifyCommand ?? (_QuestionModifyCommand = new AppCommand(QuestionModifyCommandFunc)); }
            }
            public void QuestionModifyCommandFunc(Object o)
            {
                PageWriteNote.mId = MyQuestionId;
                PageWriteNote.mContent = originContent;
                PageWriteNote.isInsertUpdate = false;

                ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageWriteNote.xaml", UriKind.Relative);
            }
            #endregion

            #region QuestionDeleteCommand
            private ICommand _QuestionDeleteCommand;
            public ICommand QuestionDeleteCommand
            {
                get { return _QuestionDeleteCommand ?? (_QuestionDeleteCommand = new AppCommand(QuestionDeleteCommandFunc)); }
            }
            public void QuestionDeleteCommandFunc(Object o)
            {
                if (MessageBox.Show("삭제하시겠습니까?", "삭제 경고", MessageBoxButton.YesNo) == MessageBoxResult.No) return; 
                dbMyQuestion.DeleteQuestion(MyQuestionId);
                PageMainSubject.MainFrameObject.Refresh();
                ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageMyQuestion.xaml", UriKind.Relative);
            }
            #endregion
        }
    }
}
