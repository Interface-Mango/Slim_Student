using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Slim_Student.Model;

namespace Slim_Student.View
{
	/// <summary>
	/// MainFrame.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainFrame : NavigationWindow
	{
        public static object[] UserInfo;
        public static MainFrame Frame;

        private DB_Subject dbSubject;
        private DB_Attendance dbAttendance;

		public MainFrame(object[] _userInfo)
		{
            InitializeComponent();
            Frame = this;
            ResizeMode = ResizeMode.NoResize;
            UserInfo = _userInfo;
            new SerialCommunication();

            dbSubject = new DB_Subject(new DBManager());
            dbAttendance = new DB_Attendance(new DBManager());

            // 창 중앙 위치!!
            this.Left = (SystemParameters.WorkArea.Width - Width)/2.0 + SystemParameters.WorkArea.Left;
            this.Height = (SystemParameters.WorkArea.Height - Height)/2.0 + SystemParameters.WorkArea.Top;
            
		}
		
		// 로그인 창과 호환되기 위한 함수
		protected override void OnClosed(EventArgs e)
        {
            // LED 끄기
            try
            {
                SerialCommunication.CurrentSignal = "n";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);// 수업시간 도중 프로그램 종료 후 재접시 다시 출석 되게 att_check 바꿈
                
                // 여기서 선생님이 로그아웃 되어있는지 체크하고 수업중인데 나가면 0으로 바꿈.
                int flag = Convert.ToInt32(dbSubject.SelectIsProcessing(Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id))));
                if (flag == 1)
                {
                    dbAttendance.UpdateAttendance(Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id).ToString(), DateTime.Now.Date.ToString().Split(' ')[0], 0);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
			base.OnClosed(e); 
			Application.Current.Shutdown();
		}
        

        public static void closeWindow() 
        {
            Frame.Close();
        }

        public static MainFrame thisMainFrame()
        {
            return Frame;
        }

        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
	}
}