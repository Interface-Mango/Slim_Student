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

namespace Slim_Student
{
	public partial class SubjectList
	{
       
		public SubjectList()
		{
			this.InitializeComponent();
		}

        private void SubStartBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/PageMainSubject.xaml", UriKind.Relative));
        }
	}
}