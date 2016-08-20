using MVVMBase.Command;
using MVVMBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Slim_Student.Model;
using Slim_Student.View;

namespace Slim_Student.ViewModel
{
    class ViewModelPageNoticeDetail : ViewModelBase
    {
        private PageNoticeDetail pNoticeDetail;

        public ViewModelPageNoticeDetail(PageNoticeDetail pDetail, int id, string std_id, int sub_id, string content, DateTime date) 
        {
            pNoticeDetail = pDetail;
            NoticeDetailContent = content;
        }

        #region NoticeDetailContent
        private string _NoticeDetailContent;
        public string NoticeDetailContent
        {
            get { return _NoticeDetailContent; }
            set
            {
                if (_NoticeDetailContent != value)
                {
                    _NoticeDetailContent = value;
                    OnPropertyChanged("NoticeDetailContent");
                }
            }
        }
        #endregion

        #region BackCommand
        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get { return _BackCommand ?? (_BackCommand = new AppCommand(BackCommandFunc)); }
        }
        public void BackCommandFunc(Object o)
        {
            ViewModelMainSubject.MainSubjectObject.FrameSource = new Uri("PageMyQuestion.xaml", UriKind.Relative);
        }
        #endregion
    }
}
