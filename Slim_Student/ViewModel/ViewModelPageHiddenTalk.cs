using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Slim_Student.Model;
using System.Net.Sockets;
using System.Net;
using System.Windows.Controls;

namespace Slim_Student.ViewModel
{
    class ViewModelPageHiddenTalk : ViewModelBase
    {
        private Socket m_ServerSocket;
        private Socket m_ClientSocket;
        private byte[] szData;

        private TextBox _textbox1;
        private TextBox _textbox2;

        public ViewModelPageHiddenTalk(TextBox textbox1, TextBox textbox2)
        {
            _textbox1 = textbox1;
            _textbox2 = textbox2;

            m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000); //포트 대기 설정

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = ipep;

            m_ClientSocket.ConnectAsync(args);
        }


        #region ShowTextBox
        private string _ShowTextBox;
        public string ShowTextBox
        {
            get { return _ShowTextBox; }
            set
            {
                _ShowTextBox = value;
                OnPropertyChanged("ShowTextBox");
            }
        }
        #endregion


        #region SendingTextBox
        private string _SendingTextBox;
        public string SendingTextBox
        {
            get { return _SendingTextBox; }
            set
            {
                _SendingTextBox = value;
                OnPropertyChanged("SendingTextBox");
            }
        }
        #endregion


        #region SendingBtn
        private ICommand _SendingBtn;
        public ICommand SendingBtn
        {
            get { return _SendingBtn ?? (_SendingBtn = new AppCommand(SendingBtnFunc)); }
        }

        private void SendingBtnFunc(Object o)
        {
            if (_textbox1.Text.Length > 0)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                byte[] szData = Encoding.Unicode.GetBytes(_textbox1.Text);
                args.SetBuffer(szData, 0, szData.Length);
                m_ClientSocket.SendAsync(args);
                _textbox1.Text = "";
                _textbox1.Focus();
            }

        }
        #endregion


        #region ReceiveServerData
        private ICommand _ReceiveServerData;
        private ICommand ReceiveServerData
        {
            get { return _ReceiveServerData ?? (_ReceiveServerData = new AppCommand(ReceiveServerDataFunc)); }
        }

        private void ReceiveServerDataFunc(Object o)
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed
                += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            m_ClientSocket.AcceptAsync(args);
        }

        private void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket ServerSocket = e.AcceptSocket;
            
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                szData = new byte[1024];
                args.SetBuffer(szData, 0, 1024);
                args.UserToken = m_ServerSocket;
                args.Completed
                    += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
                ServerSocket.ReceiveAsync(args);
            
            e.AcceptSocket = null;
            m_ClientSocket.AcceptAsync(e);
        }

        private void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket ServerSocket = (Socket)sender;

            if (ServerSocket.Connected && e.BytesTransferred > 0)
            {
                byte[] szData = e.Buffer;    // 데이터 수신
                string sData = Encoding.Unicode.GetString(szData);

                string Test = sData.Replace("\0", "").Trim();

                SetText(Test);

                for (int i = 0; i < szData.Length; i++)
                {
                    szData[i] = 0;
                }
                e.SetBuffer(szData, 0, 1024);
                ServerSocket.ReceiveAsync(e);
            }
            else
            {
                ServerSocket.Disconnect(false);
                ServerSocket.Dispose();
                
            }
        }

        private delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            //호출한 스레드가 UI 스레드인가
            if (_textbox2.Dispatcher.CheckAccess())
            {
                _textbox2.AppendText("  " + text + "\n\n");
                _textbox2.ScrollToEnd();
                _textbox2.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                //작업쓰레드면
                SetTextCallback d = new SetTextCallback(SetText);
                _textbox2.Dispatcher.BeginInvoke(d, new Object[] { text });
            }
        }
        #endregion

    }
}
