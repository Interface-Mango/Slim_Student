using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelWidgetInputWindow : ViewModelBase
    {
        private WidgetInputWindow widgetInputWindow;
        public ViewModelWidgetInputWindow(WidgetInputWindow wWindow)
        {
            widgetInputWindow = wWindow;
        }

       
    }
}
