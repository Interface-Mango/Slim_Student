using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Slim_Student.View;
using Slim_Student.Model;
using System.Net.Sockets;
using System.Net;
using System.Windows.Controls;
using SocketGlobal;

namespace Slim_Student.ViewModel
{
   
    class ViewModelPageHiddenTalk : ViewModelBase
    {
        /// 명령어 클래스
        private claCommand m_insCommand = new claCommand();

        /// 내 소켓
        private Socket m_socketMe = null;

        private PageHiddenTalk pht;
        private TextBox txtMsg;
        private string NickName;
        private TextBlock IDText;
        private Button ServerConnectingBtn;
        
        /// ServerConnectBtn 버튼 상태
        enum typeState
        {
            /// 연결하세요
            Connecting,

            /// 끊으세요
            DisConnecting,
        }

        /// 나의 상태
        private typeState m_typeState = typeState.Connecting;


        public ViewModelPageHiddenTalk
            (PageHiddenTalk _pht, TextBox _txtMsg, TextBlock _IDText, Button _ServerConnectingBtn)
        {
            pht = _pht;
            txtMsg = _txtMsg;
            IDText = _IDText;
            ServerConnectingBtn = _ServerConnectingBtn;
            NickName = randomID();

            UI_Setting(typeState.Connecting);
        }

        // UI 세팅
        private void UI_Setting(typeState typeSet)
        {
            //들어온 값을 세팅하고
            m_typeState = typeSet;


            switch (typeSet)
            {
                case typeState.Connecting:	// 연결 전
                    try
                    {
                        ServerConnectingBtn.Content = "채팅 접속";
                        txtMsg.IsEnabled = false;
                    }

                    catch(InvalidOperationException)
                    {
                        // 서버에 연결을 하지 않은채
                        // 클라이언트가 연결을 시도 할 경우
                        pht.Dispatcher.BeginInvoke(new Action(
                                delegate()
                                {
                                    ServerConnectingBtn.Content = "채팅 접속";
                                    txtMsg.IsEnabled = false;
                                }));
                    }
                    break;

                case typeState.DisConnecting:	// 연결완료
                            pht.Dispatcher.BeginInvoke(new Action(
                                delegate()
                                {
                                    ServerConnectingBtn.Content = "채팅 종료";
                                }));
                            txtMsg.IsEnabled = true;

                    break;
            }

        }


        #region msgSend
        private ICommand _msgSend;
        public ICommand msgSend
        {
            get { return _msgSend ?? (_msgSend = new AppCommand(msgSendFunc)); }
        }

        private void msgSendFunc(Object o)
        {
            //메시지를 보낸다.
            StringBuilder sbData = new StringBuilder();
            sbData.Append(claCommand.Command.Msg.GetHashCode());
            sbData.Append(claGlobal.g_Division);
            sbData.Append(txtMsg.Text);

            SendMsg(sbData.ToString());
            txtMsg.Text = "";
            txtMsg.Focus();
        }
        #endregion

        #region ServerConnect
        private ICommand _ServerConnect;
        public ICommand ServerConnect
        {
            get { return _ServerConnect ?? (_ServerConnect = new AppCommand(ServerConnectFunc));}
        }
        private void ServerConnectFunc(Object o)
        {
            switch (m_typeState)
            {
                case typeState.Connecting:
                    {
                        //UI 세팅
                        UI_Setting(typeState.DisConnecting);

                        int portNum = Convert.ToInt32(PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.port));
                        string ipaddr = PageMainSubject.SubjectInfo.ElementAt((int)DB_Subject.FIELD.ipaddr).ToString();


                        //소켓 생성
                        Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                        IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(ipaddr), portNum);

                        SocketAsyncEventArgs saeaServer = new SocketAsyncEventArgs();
                        saeaServer.RemoteEndPoint = ipepServer;
                        //연결 완료 이벤트 연결
                        saeaServer.Completed += new EventHandler<SocketAsyncEventArgs>(Connect_Completed);

                        //서버 메시지 대기
                        socketServer.ConnectAsync(saeaServer);
                        break;
                    }

                case typeState.DisConnecting:
                    {
                        UI_Setting(typeState.Connecting);
                        pht.DisplayMsg("* 채팅 종료 * ");
                        pht.Dispatcher.BeginInvoke(new Action(
                        delegate()
                        {
                            IDText.Text = "ID";
                        }));

                        m_socketMe.Close();
                        break;
                    }
            }
        }
        #endregion


        /// 연결 완료
        private void Connect_Completed(object sender, SocketAsyncEventArgs e)
        {
            m_socketMe = (Socket)sender;

            if (true == m_socketMe.Connected)
            {
                MessageData mdReceiveMsg = new MessageData();

                //서버에 보낼 객체를 만든다.
                SocketAsyncEventArgs saeaReceiveArgs = new SocketAsyncEventArgs();
                //보낼 데이터를 설정하고
                saeaReceiveArgs.UserToken = mdReceiveMsg;
                //데이터 길이 세팅
                saeaReceiveArgs.SetBuffer(mdReceiveMsg.GetBuffer(), 0, 4);
                //받음 완료 이벤트 연결
                saeaReceiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recieve_Completed);
                //받음 보냄
                m_socketMe.ReceiveAsync(saeaReceiveArgs);

                pht.DisplayMsg("* 서버 접속 성공 *");
                //서버 연결이 성공하면 id체크를 시작한다.
                
                Login(NickName);
            }
            else
            {
                Disconnection();
            }
        }

        private void Recieve_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket socketClient = (Socket)sender;
            MessageData mdRecieveMsg = (MessageData)e.UserToken;
            mdRecieveMsg.SetLength(e.Buffer);
            mdRecieveMsg.InitData();

            if (true == socketClient.Connected)
            {
                //연결이 되어 있다.

                //데이터 수신
                socketClient.Receive(mdRecieveMsg.Data, mdRecieveMsg.DataLength, SocketFlags.None);
                //명령을 분석 한다.
                MsgAnalysis(mdRecieveMsg.GetData());
                try
                {
                    //다음 메시지를 받을 준비를 한다.
                    socketClient.ReceiveAsync(e);
                }
                catch(ObjectDisposedException)
                {
                    //receiveAsync 탈출구
                }
                
            }
            else
            {
                Disconnection();
            }
        }

        /// 넘어온 데이터를 분석 한다.
        private void MsgAnalysis(string sMsg)
        {
            //구분자로 명령을 구분 한다.
            string[] sData = sMsg.Split(claGlobal.g_Division);

            //데이터 개수 확인
            if ((1 <= sData.Length))
            {
                //0이면 빈메시지이기 때문에 별도의 처리는 없다.

                //넘어온 명령
                claCommand.Command typeCommand
                    = m_insCommand.StrIntToType(sData[0]);

                switch (typeCommand)
                {
                    case claCommand.Command.None:	//없다
                        break;
                    case claCommand.Command.Msg:	//메시지인 경우
                        Command_Msg(sData[1]);
                        break;
                    case claCommand.Command.ID_Check_Ok:	//아이디 성공
                        SendMeg_IDCheck_Ok();
                        break;
                    case claCommand.Command.ID_Check_Fail:	//아이디 실패
                        SendMeg_IDCheck_Fail();
                        break;
                    case claCommand.Command.Server_Disconnection: // 서버종료를 받음
                        Disconnection();
                        break;
                }
            }
        }

        /// 메시지 출력
        private void Command_Msg(string sMsg)
        {
            pht.DisplayMsg(sMsg);
        }

        /// 아이디 성공
        private void SendMeg_IDCheck_Ok()
        {
            pht.Dispatcher.BeginInvoke(new Action(
                        delegate()
                        {
                            IDText.Text = NickName;
                        }));

            //아이디확인이 되었으면 서버에 로그인 요청을 하여 로그인을 끝낸다.
            StringBuilder sbData = new StringBuilder();
            sbData.Append(claCommand.Command.Login.GetHashCode());
            sbData.Append(claGlobal.g_Division);

            SendMsg(sbData.ToString());
        }

        /// 아이디체크 실패
        private void SendMeg_IDCheck_Fail()
        {
            NickName = randomID();
            Login(NickName);
        }

        /// 접속이 끊겼다.
        private void Disconnection()
        {
            //접속 끊김
            m_socketMe.Close();
            pht.DisplayMsg("* 서버로부터 응답이 없습니다. *\n");
            UI_Setting(typeState.Connecting);

        }

        private string randomID()
        {
            string[] NickName = 
            { "Apple ", "Banana ", "Korea ", "Babo ", "Mouse ",
                "Book ", "Lemon ", "Orange ", "멍청이 ", "이순신 ",
                "장영실 ", "Melon ", "Moon ", "Star ", "Cloud ",
                "Winter ", "Spring ", "Summer ","Fall ","광해군 ",
                "연산군 ", "논개 ", "장희빈 "
            };
            Random rand = new Random();

            int r = rand.Next(0, NickName.Length);
            return NickName[r];
        }

        /// 아이디 체크 요청

        private void Login(String Nick)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append(claCommand.Command.ID_Check.GetHashCode());
            sbData.Append(claGlobal.g_Division);

            sbData.Append(Nick);

            SendMsg(sbData.ToString());
        }




        /// 서버로 메시지를 전달 합니다.
        private void SendMsg(string sMsg)
        {
            MessageData mdSendMsg = new MessageData();

            //데이터를 넣고
            mdSendMsg.SetData(sMsg);

            //서버에 보낼 객체를 만든다.
            SocketAsyncEventArgs saeaServer = new SocketAsyncEventArgs();
            //데이터 길이 세팅
            saeaServer.SetBuffer(BitConverter.GetBytes(mdSendMsg.DataLength), 0, 4);
            //보내기 완료 이벤트 연결
            saeaServer.Completed += new EventHandler<SocketAsyncEventArgs>(Send_Completed);
            //보낼 데이터 설정
            saeaServer.UserToken = mdSendMsg;
            //보내기
            m_socketMe.SendAsync(saeaServer);
        }


        /// 메시지 보내기 완료
        private void Send_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket socketSend = (Socket)sender;
            MessageData mdMsg = (MessageData)e.UserToken;
            //데이터 보내기 마무리
            socketSend.Send(mdMsg.Data);
        }
    }
}
