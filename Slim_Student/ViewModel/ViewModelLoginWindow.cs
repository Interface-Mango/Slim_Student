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

namespace Slim_Student.ViewModel
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

        #region PWBox
        private string _PWBox;
        public string PWBox
        {
            get { return _PWBox; }
            set
            {
                if (_PWBox != value)
                {
                    _PWBox = value;
                    OnPropertyChanged("PWBox");
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
        public static bool isLogin;

        /* LoginCommandFunc
         * 기능 : 로그인 버튼 눌렀을 때 회원검사하는 함수
         */
        private void LoginCommandFunc(Object o)
        {
            object[] obj = dbUser.SelectUser(IDTextBox, PWBox);
            if (obj == null)
            {
                isLogin = false;
                MessageBox.Show("로그인 에러!");
            }                
            else
            {
                // 기존의 로그인창을 '일단' 숨겨놓고 메인프레임 호출
                // 메인프레임에서 로그인창을 닫아준다
                //TODO: obj[(int)DB_User.FIELD.pw] = string.Empty;
                isLogin = true;
                MainFrame mf = new MainFrame(obj);
                parentWindow.Hide();
                mf.ShowDialog();
            }
             
        }
        #endregion
    }
}
