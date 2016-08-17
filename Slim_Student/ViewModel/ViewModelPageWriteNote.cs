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
    class ViewModelPageWriteNote : ViewModelBase
    {
        private DBManager dbManager;
        private DB_MyQuestion dbMyQustion;
        private PageWriteNote parentWindow;

        private int id;
        private bool isInsertUpdate;    // 0: update, 1: insert

        // 생성자
        public ViewModelPageWriteNote(PageWriteNote pWindow)
        {
            dbManager = new DBManager();
            dbMyQustion = new DB_MyQuestion(dbManager);
            parentWindow = pWindow;
            isInsertUpdate = true;
        }
        public ViewModelPageWriteNote(PageWriteNote pWindow, int _id, string content)
        {
            dbManager = new DBManager();
            dbMyQustion = new DB_MyQuestion(dbManager);
            parentWindow = pWindow;
            id = _id;
            QuestionContentTextBox = content;
            isInsertUpdate = false;
        }

        #region QuestionContentTextBox
        private string _QuestionContentTextBox;
        public string QuestionContentTextBox
        {
            get
            {
                return _QuestionContentTextBox;
            }
            set
            {
                _QuestionContentTextBox = value;
            }
        }
        #endregion

        #region SaveQuestionCommand;
        private ICommand _SaveQuestionCommand;
        public ICommand SaveQuestionCommand
        {

            get { return _SaveQuestionCommand ?? (_SaveQuestionCommand = new AppCommand(SaveQuestionCommandFunc)); }
        }

        public void SaveQuestionCommandFunc(Object o)
        {
            if (QuestionContentTextBox == null) return;
            if(isInsertUpdate)
            {
                if (dbMyQustion.InsertQuestion(Convert.ToString(MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id)), Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), QuestionContentTextBox.ToString()))
                {
                    MessageBox.Show("저장되었습니다.");
                }
                else
                {
                    MessageBox.Show("실패하였습니다.");
                }
            }
            else
            {
                if (dbMyQustion.UpdateQuestion(id, QuestionContentTextBox))
                {
                    MessageBox.Show("수정되었습니다.");
                }
                else
                {
                    MessageBox.Show("실패하였습니다.");
                }
            }

            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageMyQuestion.xaml", UriKind.Relative);
        }
        #endregion
    }
}
