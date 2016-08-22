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
using Slim_Student.ViewModel;
using System.Windows.Media.Animation;

namespace Slim_Student.View
{

    public partial class PageHiddenTalk : Page
    {
        private int count;
        public PageHiddenTalk()
        {
            InitializeComponent();
            PortBox.MaxLength = 4;
            DataContext = new ViewModelPageHiddenTalk(this, txtMsg, PortBox, IDText, ServerConnectingBtn);
            textbox1.IsReadOnly = true;
            count = 0;
        }

        private delegate void SetTextCallback(String nMessage);
        public void DisplayMsg(String nMessage)
        {
            if (textbox1.Dispatcher.CheckAccess())
            {
                textbox1.AppendText(" " + nMessage + "\n");
                textbox1.ScrollToEnd();
                textbox1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                SetTextCallback d = new SetTextCallback(DisplayMsg);
                textbox1.Dispatcher.BeginInvoke(d, new Object[] { nMessage });
            }
        }

        #region portBox를 숫자만 입력하게
        private string prevText;
        private void PortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            double value;

            if (double.TryParse(textBox.Text, out value))
            {
                this.prevText = textBox.Text;
            }
            else
            {
                textBox.Text = this.prevText;
                textBox.SelectionLength = this.prevSelectionLength;
                textBox.SelectionStart = this.prevSelectionStart;
            }

        }

        int prevSelectionStart;
        int prevSelectionLength;
        private void PortBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            this.prevSelectionStart = textbox.SelectionStart;
            this.prevSelectionLength = textbox.SelectionLength;
        }

        #endregion

        private void PortBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (count == 0)
            {
                Storyboard ServerConnecting = new Storyboard();
                ServerConnecting = (Storyboard)this.Resources["ServerConnectingAnimation"];
                ServerConnecting.Begin();
                count++;
            }
        }
    }
}
