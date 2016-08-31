﻿using System;
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

            //위젯 창의 위치(왼쪽 위)
            this.Left = SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width;
            this.Top = SystemParameters.WorkArea.Height - SystemParameters.WorkArea.Height;
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


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	 MainFrame.Frame.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	WidgetInputWindow inputWindow = new WidgetInputWindow();
            inputWindow.ShowDialog();
        }

        // ? 눌렀을때 '?' 송신
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WidgetQuestion Question = new WidgetQuestion();
            Question.Show();

            try
            {
                SerialCommunication.SerialPortValue.Write("?");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        // OX 눌렀을때
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WidgetOX widgetOX = new WidgetOX();
            widgetOX.ShowDialog();
        }


    }
}
