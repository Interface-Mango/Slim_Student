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
        public PageHiddenTalk()
        {
            InitializeComponent();
            DataContext = new ViewModelPageHiddenTalk(this, txtMsg, IDText, ServerConnectingBtn);
            textbox1.IsReadOnly = true;
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

    }
}
