using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelWidgetQuestion : ViewModelBase
    {
        private WidgetQuestion widgetquestionWindow;
        public ViewModelWidgetQuestion(WidgetQuestion wWindow)
        {
            widgetquestionWindow = wWindow;
        }

        #region Close
        private ICommand _Close;
        public ICommand Close
        {
            get
            {
                return _Close ?? (_Close = new AppCommand(CloseFunc));
            }
        }
        public void CloseFunc(Object o)
        {
            widgetquestionWindow.Close();
        }
        #endregion
    }
}
