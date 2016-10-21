using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows;
using Slim_Student.Model;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelPageSignalLightMonitor : ViewModelBase
    {
        PageSignalLightMonitor parentWindow;
        public ViewModelPageSignalLightMonitor(PageSignalLightMonitor pWindow)
        {
            parentWindow = pWindow;
        }

        #region QuestionSignal
        private ICommand _QuestionSignal;
        public ICommand QuestionSignal
        {
            get { return _QuestionSignal ?? (_QuestionSignal = new AppCommand(QuestionSignalFunc)); }
        }

        private void QuestionSignalFunc(Object o)
        {
            var uriSource = new Uri(@"..\View\Images\question.png", UriKind.Relative);
            parentWindow.CurrentState.Source = new BitmapImage(uriSource);
            parentWindow.CurrentState.UpdateLayout();
            //parentWindow.TB.Text = "Question";
        }
        #endregion
    }
}
