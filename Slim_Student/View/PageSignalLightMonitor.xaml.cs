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
using System.Windows.Media.Animation;

namespace Slim_Student.View
{
    /// <summary>
    /// PageSignalLightMonitor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageSignalLightMonitor : Page
    {

        private Storyboard story1;

        public PageSignalLightMonitor()
        {
            InitializeComponent();
            DataContext = new ViewModelPageSignalLightMonitor(this);
            story1 = this.Resources["CreateNewByClick"] as Storyboard;
        }

      /*  private void numberImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Images/number.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            
        }

        private void checkImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/check.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            
        }

        private void oxImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/ox.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
          
        }

        private void questionImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Slim_Student;component/Images/quetion.png");
            CurrentState.Source = new BitmapImage(uriSource);
            CurrentState.UpdateLayout();
            
        } */

        
        private void QuestionWindow(object sender, MouseButtonEventArgs e)
        {
            WidgetQuestion Question = new WidgetQuestion(true);
            Question.ShowDialog();
            if(Question.IsRegist){ //등록되었을 때
                
                story1.Begin(this);

                try
                {
                    SerialCommunication.CurrentSignal = "?";
                    SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }


        private void OXWindow(object sender, MouseButtonEventArgs e)
        {
            WidgetOX widgetOX = new WidgetOX(true);
            widgetOX.ShowDialog();
        }


        private void NumberWindow(object sender, MouseButtonEventArgs e)
        {
            WidgetInputWindow inputWindow = new WidgetInputWindow(true);
            inputWindow.ShowDialog();
        }





    }
}
