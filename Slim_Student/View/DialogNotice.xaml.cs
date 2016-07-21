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

namespace Slim_Student
{
    /// <summary>
    /// DialogNotice.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DialogNotice : Window
    {
        public DialogNotice()
        {
            InitializeComponent();
            DataContext = new ViewModelDialogNotice();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
