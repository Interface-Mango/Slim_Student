﻿using System;
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

		public MainFrame(object[] _userInfo)
		{
            this.InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            UserInfo = _userInfo;
		}		
		
		// 로그인 창과 호환되기 위한 함수
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e); 
			Application.Current.Shutdown();
		}

		
		
	}
}