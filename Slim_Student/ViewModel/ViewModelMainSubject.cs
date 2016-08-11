using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Slim_Student.Model;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelMainSubject : ViewModelBase
    {
        public static ViewModelMainSubject MainSubjectObject;

        private SubjectList _subjectlist;
        public ViewModelMainSubject(SubjectList subjectlist)
        {
            _subjectlist = subjectlist;
            MainSubjectObject = this;
        }


        #region FrameSource
        private Uri _FrameSource;
        public Uri FrameSource
        {
            get { return _FrameSource; }
            set
            {
                if (_FrameSource != value)
                {
                    _FrameSource = value;
                    OnPropertyChanged("FrameSource");
                }
            }
        }
        #endregion


        #region GoSignalLight
        private ICommand _GoSignalLight;
        public ICommand GoSignalLight
        {
            get { return _GoSignalLight ?? (_GoSignalLight = new AppCommand(GoSignalLightFunc)); }
        }

        private void GoSignalLightFunc(Object o)
        {
            _FrameSource = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            OnPropertyChanged("FrameSource");
        }
        #endregion

        #region GoHiddenTalkCommand
        private ICommand _GoHiddenTalk;
        public ICommand GoHiddenTalk
        {
            get { return _GoHiddenTalk ?? (_GoHiddenTalk = new AppCommand(GoHiddenTalkFunc)); }
        }

        private void GoHiddenTalkFunc(Object o)
        {
            _FrameSource = new Uri("PageHiddenTalk.xaml", UriKind.Relative);
            Console.WriteLine(_FrameSource.OriginalString);
            OnPropertyChanged("FrameSource");
        }
        #endregion

        #region GoMyQuestion
        private ICommand _GoMyQuestion;
        public ICommand GoMyQuestion
        {
            get { return _GoMyQuestion ?? (_GoMyQuestion = new AppCommand(GoMyQuestionFunc)); }
        }

        private void GoMyQuestionFunc(Object o)
        {
            _FrameSource = new Uri("PageMyQuestion.xaml", UriKind.Relative);
            OnPropertyChanged("FrameSource");
        }
        #endregion

        #region GoQna
        private ICommand _GoQna;
        public ICommand GoQna
        {
            get { return _GoQna ?? (_GoQna = new AppCommand(GoQnaFunc)); }
        }

        private void GoQnaFunc(Object o)
        {
            _FrameSource = new Uri("PageQnA.xaml", UriKind.Relative);
            OnPropertyChanged("FrameSource");
        }
        #endregion

        #region GoNotice
        private ICommand _GoNotice;
        public ICommand GoNotice
        {
            get { return _GoNotice ?? (_GoNotice = new AppCommand(GoNoticeFunc)); }
        }

        private void GoNoticeFunc(Object o)
        {
            _FrameSource = new Uri("PageNotice.xaml", UriKind.Relative);
            OnPropertyChanged("FrameSource");
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

        #region GoHome
        private ICommand _GoHome;
        private MainFrame mf;
        public ICommand GoHome
        {
            get { return _GoHome ?? (_GoHome = new AppCommand(GoHomeFunc)); }
        }

        public void GoHomeFunc(object o)
        {
            mf = MainFrame.thisMainFrame();
            mf.NavigationService.Navigate(_subjectlist);
        }
        #endregion


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

    }
}
