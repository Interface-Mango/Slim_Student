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
            TextBoxBuffer.Focus();
            DataContext = new ViewModelWidgetInputWindow(this);
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(TextBoxBuffer.Text));
            try
            {
                SerialCommunication.SerialPortValue.Write(TextBoxBuffer.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
