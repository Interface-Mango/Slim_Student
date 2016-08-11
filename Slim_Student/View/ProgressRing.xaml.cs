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
        public ProgressRing()
        {
            InitializeComponent();
			AlwaysProgressRing();
        }

        public void AlwaysProgressRing()
        {
            Storyboard progressring;
            progressring = (Storyboard)this.Resources["AlwaysAnimation"];
            
			progressring.RepeatBehavior = RepeatBehavior.Forever;
            progressring.Begin(this);
        }


        public Storyboard MangoProgressBar()
        {
            Storyboard mangoanimation;
            mangoanimation = (Storyboard)this.Resources["MangoAnimation"];
            return mangoanimation;
        }

        public void mangoAnimationStop(Storyboard m)
        {
            m.FillBehavior = FillBehavior.HoldEnd;
           
        }
		
		public void AutoClose(object sender, EventArgs e)
		{
            this.Close();
		}


    }
}
