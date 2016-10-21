using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Slim_Student.ViewModel;

namespace Slim_Student.View
{
    /// <summary>
    /// PageSignalLightMonitor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageSignalLightMonitor : Page
    {
        public PageSignalLightMonitor()
        {
            InitializeComponent();
            DataContext = new ViewModelPageSignalLightMonitor(this);
        }

        private void numberImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Images/number.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            TB.Text = "number";
        }

        private void checkImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/check.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            TB.Text = "check";
        }

        private void oxImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/ox.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            TB.Text = "ox";
        }

        private void questionImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/quetion.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            TB.Text = "Question";
        }
    }
}
