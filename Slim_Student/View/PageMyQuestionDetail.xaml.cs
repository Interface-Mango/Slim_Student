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

namespace Slim_Student.View
{
    /// <summary>
    /// PageMyQuestionDetail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageMyQuestionDetail : Page
    {
        public static int mId;
        public static string mStdId;
        public static int mSubId;
        public static string mContent;
        public static DateTime mDate;
        
        public PageMyQuestionDetail()
        {
            InitializeComponent();
            //DataContext = new ViewModelPageMyQuestionDetail(this);
            DataContext = new ViewModelPageMyQuestionDetail(this, mId, mStdId, mSubId, mContent, mDate);
        }
    }
}
