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
using System.Windows.Media.Animation;


namespace Slim_Student.View
{
    /// <summary>
    /// WidgetQuestion.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WidgetQuestion : Window
    {
        public static int mId;
        public static string mContent;
        public static bool isInsertUpdate;

        private bool _isRegist = false;
        private WidgetQuestion widgetQuestion;

       
        public WidgetQuestion()
        {
            InitializeComponent();
            if (isInsertUpdate)  // 새로 삽입
                DataContext = new WidgetQuestion(this);
       
        }

        public WidgetQuestion(bool state)
        {
            InitializeComponent();
            
            DataContext = new ViewModelWidgetQuestion(this);

            if (state == false)
            {
                this.Left = SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width;
                this.Top = 55.0;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            } 

        }

        public WidgetQuestion(WidgetQuestion widgetQuestion)
        {
            this.widgetQuestion = widgetQuestion;
        }

        public void AutoClose(object sender, EventArgs e)
        {
         
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODOHJ - DB로 올리기 
            try
            {
                SerialCommunication.CurrentSignal = "g";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           

            this.Close();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SerialCommunication.CurrentSignal = "g";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
