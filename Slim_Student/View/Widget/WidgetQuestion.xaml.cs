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

        private bool _isRegist = false;
        public bool IsRegist
        { 
            get { return _isRegist; }
            set
            {
                _isRegist = value;
            }
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

        public void AutoClose(object sender, EventArgs e)
        {
            IsRegist = false;
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsRegist = true;
            this.Close();
        }

    }
}
