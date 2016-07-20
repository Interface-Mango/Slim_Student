using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Slim_Student
{
    class ViewModelMainSubject : ViewModelBase
    {
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


        #region ProfileName
        private string _ProfileName;
        public string ProfileName
        {
            get { return _ProfileName; }
        }
        #endregion

        #region ProfileGroup
        private string _ProfileGroup;
        public string ProfileGroup
        {
            get { return _ProfileGroup; }
        }
        #endregion

    }
}
