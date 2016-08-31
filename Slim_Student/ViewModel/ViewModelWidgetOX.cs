using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slim_Student.View;
using MVVMBase.ViewModel;
using System.Windows.Input;
using MVVMBase.Command;
using System.Windows.Controls;

namespace Slim_Student.ViewModel
{
    class ViewModelWidgetOX : ViewModelBase
    {
        private WidgetOX widgetOX;
       
        public ViewModelWidgetOX(WidgetOX wWindow)
        {
            widgetOX = wWindow;
        }

        #region Oclick
        private ICommand _Oclick;
        public ICommand Oclick
        {
            get 
            {
                return _Oclick ?? (_Oclick = new AppCommand(OclickFunc));
            }
        }
        private void OclickFunc(Object o)
        {
            widgetOX.textBox.Text = "O"; 
        }
        #endregion

        #region Xclick
        private ICommand _Xclick;
        public ICommand Xclick
        {
            get
            {
                return _Xclick ?? (_Oclick = new AppCommand(XclickFunc));
            }
        }
        private void XclickFunc(Object o)
        {
            widgetOX.textBox.Text = "X";
        }
        #endregion


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

                SerialCommunication.SerialPortValue.Write(widgetOX.textBox.Text);
                widgetOX.textBox.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        #endregion


    }

}
