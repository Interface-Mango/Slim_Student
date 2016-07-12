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

namespace Slim_professor
{
	public partial class NoticePage
	{
		public NoticePage()
		{
			this.InitializeComponent();		
		}

        private void NoticeTitleBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowNotice sn = new ShowNotice();
            sn.ShowDialog();
        }

        private void SignalLightMonitorBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SignalLightMonitorPage.xaml", UriKind.Relative));
        }

        private void HiddenTalkBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("HiddenTalkPage.xaml", UriKind.Relative));
        }

        private void MyQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MyQuestionPage.xaml", UriKind.Relative));
        }

        private void QnABtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("QnAPage.xaml", UriKind.Relative));
        }

		

	}
}