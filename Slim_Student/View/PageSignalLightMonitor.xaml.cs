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
        private string CurrentSign;

        public PageSignalLightMonitor()
        {
            InitializeComponent();
            DataContext = new ViewModelPageSignalLightMonitor(this);
            Clock();
           
        }

      

        
        private void QuestionWindow(object sender, MouseButtonEventArgs e)
        {
            WidgetQuestion Question = new WidgetQuestion(true);
            Question.ShowDialog();

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




        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                SerialCommunication.CurrentSignal = "V";
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        #region Timer
        public void Clock()
        {
            System.Windows.Threading.DispatcherTimer TimerClock = new System.Windows.Threading.DispatcherTimer();

            const int TIMER_VALUE = 2;

            TimerClock.Interval = new TimeSpan(0, 0, 0, TIMER_VALUE);
            TimerClock.IsEnabled = true;
            TimerClock.Tick += new EventHandler(TimerClock_Tick);
        }

        public void TimerClock_Tick(object sender, EventArgs e)
        {
            CurrentSign = SerialCommunication.CurrentSignal;

            if (CurrentSign == "r")
            {
                var uriSource = new Uri(@"..\View\Images\mangoRed.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }
            else if (CurrentSign == "g")
            {
                var uriSource = new Uri(@"..\View\Images\mangoGreen.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }
            else if (CurrentSign == "0")
            {
                var uriSource = new Uri(@"..\View\Images\0.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "1")
            {
                var uriSource = new Uri(@"..\View\Images\1.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "2")
            {
                var uriSource = new Uri(@"..\View\Images\2.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "3")
            {
                var uriSource = new Uri(@"..\View\Images\3.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "4")
            {
                var uriSource = new Uri(@"..\View\Images\4.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "5")
            {
                var uriSource = new Uri(@"..\View\Images\5.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "6")
            {
                var uriSource = new Uri(@"..\View\Images\6.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "7")
            {
                var uriSource = new Uri(@"..\View\Images\7.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "8")
            {
                var uriSource = new Uri(@"..\View\Images\8.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();
            }

            else if (CurrentSign == "9")
            {
                var uriSource = new Uri(@"..\View\Images\9.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();

            }

            else if (CurrentSign == "V")
            {
                var uriSource = new Uri(@"..\View\Images\check.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();

            }
            else if (CurrentSign == "O")
            {
                var uriSource = new Uri(@"..\View\Images\o.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();

            }
            else if (CurrentSign == "X")
            {
                var uriSource = new Uri(@"..\View\Images\xx.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();

            }
            else if (CurrentSign == "?")
            {
                var uriSource = new Uri(@"..\View\Images\question.png", UriKind.Relative);
                CurrentState.Source = new BitmapImage(uriSource);
                CurrentState.UpdateLayout();

            }

        }

        #endregion



    }
}
