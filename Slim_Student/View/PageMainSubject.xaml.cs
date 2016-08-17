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
using Slim_Student.View;

namespace Slim_Student.View
{
    /// <summary>
    /// PageMainSubject.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageMainSubject : Page
    {
        public static object[] SubjectInfo;
        public static Frame MainFrameObject;

        public PageMainSubject(object[] param, SubjectList subjectlist)
        {
            InitializeComponent();
            DataContext = new ViewModelMainSubject(subjectlist);

            MainFrameObject = FramePanel;
            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            //FramePanel.Source = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            SubjectInfo = param;
            SubName.Text = SubjectInfo.ElementAt(1).ToString();
        }
    }
}
