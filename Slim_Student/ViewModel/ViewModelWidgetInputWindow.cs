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

        #region SendCommand
        private ICommand _SendCommand;
        public ICommand SendCommand
        {
            get
            {
                return _SendCommand ?? (_SendCommand = new AppCommand(SendCommandFunc));
            }
        }
        public void SendCommandFunc(Object o)
        {
            //Console.WriteLine(Convert.ToChar(widgetInputWindow.tbNum.Text));
            try
            {
                SerialCommunication.CurrentSignal = widgetInputWindow.tbNum.Text;
                SerialCommunication.SerialPortValue.Write(SerialCommunication.CurrentSignal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        #endregion
    }
}
