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
using System.Windows.Shapes;
using System.IO.Ports;

namespace Slim_Student.View
{
    /// <summary>
    /// Widget.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Widget : Window
    {/*
        private SerialPort serial = new SerialPort();
        private string g_RecvData = String.Empty;
        private delegate void SetTextCallBack(string text);
        private string[] ports;
        private int portNum = 0;*/

        public Widget()
        {
            InitializeComponent();
            //Loaded += new RoutedEventHandler(InitSerialPort);
        }
        /*
        public void InitSerialPort(object sender, EventArgs e)
        {
            serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);

            ports = SerialPort.GetPortNames();

            if (serial.IsOpen)
                serial.Close();
            try
            {
                if(ports.Length == 0)
                {
                    MessageBox.Show("연결된 포트가 없습니다.");
                    return;
                }
                else if (ports.Length == 1)
                {
                    serial.PortName = ports[portNum];
                }                
                    
                serial.BaudRate = 9600;
                serial.Open();
                MessageBox.Show("portName:" + serial.PortName + "baudRate:" + serial.BaudRate+" - 통신 성공!");
            }catch(Exception){
                portNum++;
                if (ports.Length == portNum)
                {
                    portNum = 0;
                }
                MessageBox.Show("Serial Port 열기 실패! 다시 시도해보세요.");  
            }

        }
        */
        //
        // 시리얼 데이터 수신시 발생되는 이벤트 핸들러
        ///
        /*public void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                g_RecvData = serial.ReadExisting();
                if (g_RecvData != string.Empty)
                {
                    //SetText(g_RecvData);
                    BtnNumber.Content = g_RecvData;
                }
            }
            catch (TimeoutException)
            {
                g_RecvData = string.Empty;
            }
        }*/

        /*/
        // 수신 데이터 처리 메소드
        //
        private void SetText(string text)
        {
            if (BtnNumber.Content.Dispatcher.CheckAccess())
            {
                tbRecvData.AppendText(text);
            }
            else
            {
                SetTextCallBack d = new SetTextCallBack(SetText);
                tbRecvData.Dispatcher.Invoke(d, new object[] { text });
            }
        }*/

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Frame.Show();
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnStatus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnQuestionMark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNumber_Click(object sender, RoutedEventArgs e)
        {
            WidgetInputWindow inputWindow = new WidgetInputWindow();
            inputWindow.ShowDialog();
        }

        private void BtnOX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
