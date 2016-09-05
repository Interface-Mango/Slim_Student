﻿using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private DB_Attendance dbAttendance;
        private DB_Subject dbSubject;

        public ViewModelMainSubject(SubjectList subjectlist)
        {
            _subjectlist = subjectlist;
            MainSubjectObject = this;
            dbAttendance = new DB_Attendance(new DBManager());
            dbSubject = new DB_Subject(new DBManager());
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
            FrameSource = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
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
            FrameSource = new Uri("PageHiddenTalk.xaml", UriKind.Relative);
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
            FrameSource = new Uri("PageMyQuestion.xaml", UriKind.Relative);
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
            FrameSource = new Uri("PageQnA.xaml", UriKind.Relative);
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
            FrameSource = new Uri("PageNotice.xaml", UriKind.Relative);
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
        private MainFrame mainFrame;
        private ICommand _GoHome;
        public ICommand GoHome
        {
            get { return _GoHome ?? (_GoHome = new AppCommand(GoHomeFunc)); }
        }

        public void GoHomeFunc(object o)
        {
            // LED 끄기
            try
            {
                SerialCommunication.SerialPortValue.Write("n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (Convert.ToInt32(dbSubject.SelectIsProcessing(Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)))) == 1)
            {
                dbAttendance.UpdateAttendance(Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id).ToString(), DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, 0);
            }
        
            mainFrame = MainFrame.thisMainFrame();
            mainFrame.NavigationService.Navigate(_subjectlist);
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
