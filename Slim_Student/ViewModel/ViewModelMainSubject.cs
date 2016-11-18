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

        public static int CountValue;

        private SubjectList _subjectlist;

        private DB_Attendance dbAttendance;
        private DB_Subject dbSubject;
        private DB_OnetimeProgram dbOneTime;
        private DB_AllProgram dbAllProgram;
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
            dbOneTime = new DB_OnetimeProgram(new DBManager());
            dbAllProgram = new DB_AllProgram(new DBManager());
            CountValue = 0;
            makeRedGreenList();

            Clock();

            cpu_Counter = new PerformanceCounter("Process", "% User Time", Process.GetCurrentProcess().ProcessName);
        }

        #region makeList
        public void makeRedGreenList()
        {

            object[] subItems = PageMainSubject.SubjectInfo;
            int sub_id = Convert.ToInt32(subItems[(int)DB_Subject.FIELD.sub_id]);

            ItemRedList = dbAllProgram.SelectAllProgramList(Convert.ToInt32(sub_id), 0);
            ItemGreenList = dbAllProgram.SelectAllProgramList(Convert.ToInt32(sub_id), 1);


        } 
        #endregion


        #region ItemRedList
        private List<object[]> _ItemRedList;
        public List<object[]> ItemRedList
        {
            get { return _ItemRedList; }
            set { _ItemRedList = value; }
        }
        #endregion


        #region ItemGreenList
        private List<object[]> _ItemGreenList;
        public List<object[]> ItemGreenList
        {
            get { return _ItemGreenList; }
            set { _ItemGreenList = value; }
        }
        #endregion


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


        #region ClosingWindowCommand
        private ICommand _ClosingWindowCommand;
        public ICommand ClosingWindowCommand
        {
            get { return _ClosingWindowCommand ?? (_ClosingWindowCommand = new AppCommand(ClosingWindowFunc)); }
        }

        private void ClosingWindowFunc(Object o)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            try
            {
                SerialCommunication.CurrentSignal = "n";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            MainFrame.Frame.Close();
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
                SerialCommunication.CurrentSignal = "n";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
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
            System.Windows.Threading.DispatcherTimer TimerClock = new System.Windows.Threading.DispatcherTimer();

            const int TIMER_VALUE = 2;
            /*
            TimerClock.Interval = new TimeSpan(0, 0, 0, TIMER_VALUE);
            TimerClock.IsEnabled = true;
            TimerClock.Tick += new EventHandler(TimerClock_Tick);*/
            TimerClock.Interval = TimeSpan.FromSeconds(TIMER_VALUE);
            TimerClock.Tick += new EventHandler(TimerClock_Tick);
            TimerClock.Start();
        }

        public void TimerClock_Tick(object sender, EventArgs e)
        {
        //    if (SerialCommunication.CurrentSignal == "n")
        //        return;
            handle = GetForegroundWindow();        // 활성화 윈도우
            GetWindowThreadProcessId(handle, out pid); // 핸들로 프로세스아이디 얻어옴 
            ps = Process.GetProcessById((int)pid); // 프로세스아이디로 프로세스 검색

            _temp.Text = ps.ProcessName;
            try // 체크도중 프로세스가 꺼지는 경우의 예외처리
            {
                //float cpuUsage = cpu_Counter.NextValue();
                //if (cpuUsage >= 1.0)   // 3%이상이면 ..(YES)
                //{
                    #region 알고리즘 설명
                    // 1. OneTimeDB 불러오기
                    // List<object[]> oneTimeList = dbOneTime.SelectOneTimeList(sub_id);
                    // if(oneTimeList != null)    // OnetimeDB에 YES
                    // {
                    //     if(check필드가 1이면 )   
                    //     { 
                    //          GREEN라이트
                    //          return;
                    //     }
                    //     else {
                    //          AllProgramDB 불러오기
                    //          if(Allprogram에 데이터 없으면) {
                    //              red라이트;
                    //              return;
                    //          } else {
                    //              red_green 필드 조사
                    //              if(red_green == 1) red라이트
                    //              else green라이트
                    //          }
                    //     }
                    // }
                    // else  // OnetimeDB에 NO - // OnetimeDB에 쌓이지 않은 프로세스일 경우
                    // { 
                    //     if (AllProgramDB){
                    //          1-1. AllProgramDB 불러오기  
                    //     }
                    // }
                    //  
                    #endregion

                    if (SerialCommunication.CurrentSignal == "n")
                        return;
                    else if (SerialCommunication.CurrentSignal != "r" && SerialCommunication.CurrentSignal != "g")  // r과 g 외의 신호00 시간 늘리기
                    {
                        if (0 <= CountValue && CountValue < 5)
                        {
                            CountValue++;
                            return;
                        }
                        CountValue = 0;
                    }
                    List<object[]> oneTimeList = dbOneTime.SelectOneTimeList(Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), ps.ProcessName);   //
                    if (oneTimeList != null)       // OnetimeDB에 YES (같은 이름의 프로세스가 존재하면)
                    {
                        for (int i = 0; i < oneTimeList.Count; i++)
                        {
                            if (Convert.ToInt32(oneTimeList[i].ElementAt((int)DB_OnetimeProgram.FIELD.check_field)) == 1)
                            {
                                try
                                {

                                    if (SerialCommunication.CurrentSignal == "n")
                                        return;
                                    SerialCommunication.CurrentSignal = "g";
                                    SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                return;
                            }
                            else
                            {
                                CheckAllProgramDB();
                            }
                        }
                    }
                    else    // OnetimeDB에 NO - // OnetimeDB에 쌓이지 않은 프로세스일 경우
                    {
                        CheckAllProgramDB();
                    }
                //}
                // 3%를 넘지 않으면..(NO) 그냥 지나가면 됨

                //현재 활성화 되어있는 process의 이름으로 cpu_Counter InstanceName에 대입
                cpu_Counter.InstanceName = ps.ProcessName;
            }
            catch (Exception)
            {
                return;
            }

        }

        public void CheckAllProgramDB()
        {
            if (ItemRedList == null) return;
            if (ItemGreenList == null) return;

            for (int i = 0; i < ItemRedList.Count; i++)
            {
                if (ItemRedList[i].ElementAt((int)DB_AllProgram.FIELD.process_name).ToString() == ps.ProcessName)
                {
                    try
                    {
                        SerialCommunication.CurrentSignal = "r";
                        SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    return;
                }
            }

            for (int i = 0; i < ItemGreenList.Count; i++)
            {
                if (ItemGreenList[i].ElementAt((int)DB_AllProgram.FIELD.process_name).ToString() == ps.ProcessName)
                {
                    try
                    {
                        SerialCommunication.CurrentSignal = "g";
                        SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    return;
                }
            }

            try
            {
                SerialCommunication.CurrentSignal = "r";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            
        }

        #endregion

    }
}