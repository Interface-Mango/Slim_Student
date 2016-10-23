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
    /// WidgetOX.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WidgetOX : Window
    {
        public WidgetOX(bool state)
        {
            InitializeComponent();

            if (state == false)
            {
                this.Left = SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width;
                this.Top = 35.0;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            }

            
            DataContext = new ViewModelWidgetOX(this);

        }

        public void AutoClose(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
