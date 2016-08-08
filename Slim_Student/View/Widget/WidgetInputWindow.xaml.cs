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
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Convert.ToChar(TextBoxBuffer.Text));
            try
            {
                byte[] b = new byte[30];
                SerialCommunication.SerialPortValue.Write(TextBoxBuffer.Text);
                SerialCommunication.SerialPortValue.Read(b, 0, 30);
                for (int i = 0; i < 10;i++ )
                    Console.WriteLine("b:" + b[i]);
                Console.WriteLine("=================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            this.Close();
        }
    }
}
