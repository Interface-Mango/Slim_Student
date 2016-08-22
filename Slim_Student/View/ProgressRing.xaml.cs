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
using System.Windows.Media.Animation;

namespace Slim_Student.View
{
    /// <summary>
    /// Interaction logic for ProgressRing.xaml
    /// </summary>
    public partial class ProgressRing : Window
    {
        private object[] obj;
        public ProgressRing(object[] _obj)
        {
            InitializeComponent();
            obj = _obj;
        }

		public void AutoClose(object sender, EventArgs e)
		{
            
            MainFrame mf = new MainFrame(obj);
            mf.Show();
            this.Close();
		}


    }
}
