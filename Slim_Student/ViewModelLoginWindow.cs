using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Slim_Student
{
    class ViewModelLoginWindow : ViewModelBase

    {
        private DBManager dbManager;
        private DB_User dbUser;
        private Window parentWindow;
        
        public ViewModelLoginWindow(Window pWindow)
        {
            dbManager = new DBManager();
            dbUser = new DB_User(dbManager);
            parentWindow = pWindow;
        }

    

        #region IDTextBox
        private string _IDTextBox;
        public string IDTextBox
        {
            get { return _IDTextBox; }
            set
            {
                if(_IDTextBox != value)
                {
                    _IDTextBox = value;
                    OnPropertyChanged("IDTextBox");
                }
            }
        }
        #endregion



        #region LoginCommand
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get { return _LoginCommand ?? (_LoginCommand = new AppCommand(LoginCommandFunc)); }
        }

        /* LoginCommandFunc
         * 기능 : 로그인 버튼 눌렀을 때 회원검사하는 함수
         */
        private void LoginCommandFunc(Object o)
        {
            object[] obj = dbUser.SelectUser(IDTextBox, MainWindow.pwText);

            MainWindow.pwText = string.Empty;

            if (obj == null)
            {
                MessageBox.Show("로그인 에러!");
            }                
            else
            {
                // 기존의 로그인창을 '일단' 숨겨놓고 메인프레임 호출
                // 메인프레임에서 로그인창을 닫아준다
                MainFrame mf = new MainFrame();
                parentWindow.Hide();
                mf.ShowDialog();
            }
             
        }
        #endregion
    }
}
