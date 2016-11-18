using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Slim_Student.ViewModel;
using Slim_Student.View;
using Slim_Student.Model;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace Slim_Student.View
{
    /// <summary>
    /// PageMainSubject.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageMainSubject : Page
    {
        public static object[] SubjectInfo;
        public static Frame MainFrameObject;

        private DB_Attendance dbAttendance;
    

        

        public PageMainSubject(object[] param, SubjectList subjectlist)
        {
            InitializeComponent();
            SubjectInfo = param;
            DataContext = new ViewModelMainSubject(subjectlist, temp1);
            
            MainFrameObject = FramePanel;
            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            SubName.Text = SubjectInfo.ElementAt(1).ToString();

            dbAttendance = new DB_Attendance(new DBManager());
            // 1. 초록불 키기
            try
            {
                SerialCommunication.CurrentSignal = "g";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // 2. 출석 체크 DB에 쌓기 
            List<object[]> selectList = dbAttendance.SelectAttendanceList(Convert.ToInt32(SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id).ToString(), DateTime.Now.Date.ToString().Split(' ')[0]);   // 날짜를 넣어서 시간빼고 비교

            //if (selectList != null){
            
                string temp = selectList[0].ElementAt((int)DB_Attendance.FIELD.date).ToString();
                string date = temp.Split(' ')[0];
                if (date == DateTime.Now.Date.ToString().Split(' ')[0])
                {
                    // 수업시간 도중 프로그램 종료 후 재접시 다시 출석 되게 att_check 바꿈
                    dbAttendance.UpdateAttendance(Convert.ToInt32(SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)), MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id).ToString(), DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, 1);
                    //MessageBox.Show("재접속 출석처리 되었습니다.");
                }
           
               
                
            /*}
            else
            {
                dbAttendance.InsertStudentAttendance(MainFrame.UserInfo.ElementAt((int)DB_User.FIELD.user_id).ToString(),
                                                     Convert.ToInt32(SubjectInfo.ElementAt((int)DB_Subject.FIELD.sub_id)),
                                                     1);   
            } */
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Frame.WindowState = System.Windows.WindowState.Minimized;
        }

    }
}
