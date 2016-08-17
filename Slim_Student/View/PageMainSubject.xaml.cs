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

        private MainFrame mf;
        private SubjectList _subjectlist;

        public PageMainSubject(object[] param, SubjectList subjectlist)
        {
            InitializeComponent();
            DataContext = new ViewModelMainSubject(subjectlist);

            MainFrameObject = FramePanel;
            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            //FramePanel.Source = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
            SubjectInfo = param;
            SubName.Text = SubjectInfo.ElementAt(1).ToString();
            _subjectlist = subjectlist;
        }

        #region 메뉴 버튼들
        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FramePanel.Source = new Uri("PageSignalLightMonitor.xaml", UriKind.Relative);
        }

        

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FramePanel.Source = new Uri("PageHiddenTalk.xaml", UriKind.Relative);
        }

        private void canvas2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FramePanel.Source = new Uri("PageMyQuestion.xaml", UriKind.Relative);
        }

        private void canvas3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FramePanel.Source = new Uri("PageNotice.xaml", UriKind.Relative);
        }

        private void WidgetBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Widget widget = new Widget();
            widget.Show();
            MainFrame.Frame.Hide();
        }

        private void HomeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mf = MainFrame.thisMainFrame();
            mf.NavigationService.Navigate(_subjectlist);
        }

        

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Frame.Close();
        }
        #endregion
    }
}
