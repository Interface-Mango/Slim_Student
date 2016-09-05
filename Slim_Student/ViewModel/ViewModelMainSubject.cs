using MVVMBase.Command;
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;


namespace Slim_Student.ViewModel
{
    class ViewModelMainSubject : ViewModelBase
    {
        public static ViewModelMainSubject MainSubjectObject;

        private SubjectList _subjectlist;

        private DB_Attendance dbAttendance;
        private DB_Subject dbSubject;
        private TextBox _temp;

        

        #region Algorithm component
        private PerformanceCounter cpu_Counter; // cpu 점유율
        private IntPtr handle;//활성화 윈도우를 담는 그릇
        private uint pid; // processID 얻어오기위한 그릇
        private Process ps; // pid로 프로세스 검색하기 위한 변수
        #region WindowProcessID
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWMAXIMIZED = 3;
        #endregion
        #endregion

        public ViewModelMainSubject(SubjectList subjectlist,TextBox temp)
        {
            _temp = temp;
            _subjectlist = subjectlist;
            MainSubjectObject = this;
            dbAttendance = new DB_Attendance(new DBManager());
            dbSubject = new DB_Subject(new DBManager());
            Clock();

            cpu_Counter = new PerformanceCounter("Process", "% User Time", Process.GetCurrentProcess().ProcessName);
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


        #region Algorithm
        public void Clock()
        {
            System.Windows.Threading.DispatcherTimer TimerClock =
                new System.Windows.Threading.DispatcherTimer();

            TimerClock.Interval = new TimeSpan(0, 0, 0, 1);
            TimerClock.IsEnabled = true;
            TimerClock.Tick += new EventHandler(TimerClock_Tick);
        }

        public void TimerClock_Tick(object sender, EventArgs e)
        {
            handle = GetForegroundWindow();        // 활성화 윈도우
            GetWindowThreadProcessId(handle, out pid); // 핸들로 프로세스아이디 얻어옴 
            ps = Process.GetProcessById((int)pid); // 프로세스아이디로 프로세스 검색

            if (cpu_Counter.NextValue() >= 2)
            {
                //임시 박스에 임시적으로...!
                _temp.AppendText(ps.ProcessName + Environment.NewLine );
            }

            //현재 활성화 되어있는 process의 이름으로 cpu_Counter InstanceName에 대입
            cpu_Counter.InstanceName = ps.ProcessName;

        }

        #endregion

    }
}
