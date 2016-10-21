using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Slim_Student
{
    class SerialCommunication : SerialPort
    {
        public static SerialPort SerialPortValue;
        public static string CurrentSignal;

        private string g_RecvData = string.Empty;
        private delegate void SetTextCallBack(string text);
        private string[] ports;
        private int portNum = 0;

        public SerialCommunication()
        {
            //Loaded += new RoutedEventHandler(InitSerialPort);
            InitSerialPort();
        }
        public void InitSerialPort()
        {
            //serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
            
            ports = SerialPort.GetPortNames();

            if (IsOpen)
                Close();
            try
            {
                if (ports.Length == 0)
                {
                    Console.WriteLine("연결된 포트가 없습니다.");
                    return;
                }
                else if (ports.Length == 1)
                {
                    PortName = ports[portNum];
                }

                BaudRate = 9600;
                Open();
                Console.WriteLine("portName:" + PortName + "baudRate:" + BaudRate + " - 통신 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                portNum++;
                if (ports.Length == portNum)
                {
                    portNum = 0;
                }
                Console.WriteLine("Serial Port 열기 실패! 다시 시도해보세요.");
            }

            SerialPortValue = this; //static 선언
        }

        /*/
        // 시리얼 데이터 수신시 발생되는 이벤트 핸들러
        //
        public void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
    }
}
