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
using System.Windows.Shapes;
using Slim_Student.ViewModel;

namespace Slim_Student.View
{
    /// <summary>
    /// WidgetInputWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WidgetInputWindow : Window
    {
        public WidgetInputWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            DataContext = new ViewModelWidgetInputWindow(this);

            this.Left = SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width;
            this.Top = 35.0;

            

            tbNum.IsReadOnly = true;
        }

        #region simple function
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "0";
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "1";
        }

        private void canvas2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "2";
        }

        private void canvas3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "3";
        }

        private void canvas4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "4";
        }

        private void canvas5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "5";
        }

        private void canvas6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "6";
        }

        private void canvas7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "7";
        }

        private void canvas8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "8";
        }

        private void canvas9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNum.Text = "9";
        }

        private void canvas10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
