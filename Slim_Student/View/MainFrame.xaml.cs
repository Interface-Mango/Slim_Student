using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Slim_Student
{
	/// <summary>
	/// MainFrame.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainFrame : NavigationWindow
	{
        public static object[] UserInfo;
        public static MainFrame mFrame;
        
		public MainFrame(object[] _userInfo)
		{
            this.InitializeComponent();
            mFrame = this;
            ResizeMode = ResizeMode.NoResize;
            UserInfo = _userInfo;
            //
            
		}
        

        public static void closeWindow() 
        {
            mFrame.Close();
        }

        public static MainFrame thisMainFrame()
        {
            return mFrame;
        }

		
        
		
	
	}
}