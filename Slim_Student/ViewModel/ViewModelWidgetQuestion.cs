using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Slim_Student.View;
using Slim_Student.Model;

using System.Windows;



namespace Slim_Student.ViewModel
{
    //TODOHJ프로퍼티 추가하고 
    class ViewModelWidgetQuestion : ViewModelBase
    {

        private DBManager dbManager;
        private DB_MyQuestion dbMyQustion;     

        private WidgetQuestion widgetquestionWindow;
        public ViewModelWidgetQuestion(WidgetQuestion wWindow)
        {
            dbManager = new DBManager();
            dbMyQustion = new DB_MyQuestion(dbManager);
            widgetquestionWindow = wWindow;
        }

       

        #region _WAnswer
        private string _WAnswer;
        public string WAnswer
        {
            get
            {
                return _WAnswer;
            }
            set
            {
                _WAnswer = value;
            }
        }
        #endregion

        #region SaveQuestionCommand;
        private ICommand _SaveQuestionCommand;
        public ICommand SaveQuestionCommand
        {

            get { return _SaveQuestionCommand ?? (_SaveQuestionCommand = new AppCommand(SaveQuestionCommandFunc)); }
        }
        #endregion

        public void SaveQuestionCommandFunc(Object o)
        {
            if (WAnswer == null) return;
           
                if (dbMyQustion.InsertQuestion(Convert.ToString(MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id)), Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), WAnswer.ToString()))
                {
                    MessageBox.Show("저장되었습니다.");
                }
                else
                {
                    MessageBox.Show("실패하였습니다.");
                }
            
        }


        #region Close
        private ICommand _Close;
        public ICommand Close
        {
            get
            {
                return _Close ?? (_Close = new AppCommand(CloseFunc));
            }
        }
        public void CloseFunc(Object o)
        {
            widgetquestionWindow.Close();
        }
        #endregion 
    }
}
