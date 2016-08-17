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
    /// <summary>
    /// PageMyQuestionDetail
    /// 나의 질문에서 선택한 하나의 항목에 대한 세부사항
    /// </summary>
    class ViewModelPageMyQuestionDetail : ViewModelBase
    {
        private PageMyQuestionDetail pMyQuestionDetail;

        public ViewModelPageMyQuestionDetail(PageMyQuestionDetail pDetail, int id, string std_id, int sub_id, string content, DateTime date) 
        {
            pMyQuestionDetail = pDetail;
            MyQuestionDetailContent = content;
        }

        #region MyQuestionDetailContent
        private string _MyQuestionDetailContent;
        public string MyQuestionDetailContent
        {
            get { return _MyQuestionDetailContent; }
            set
            {
                if (_MyQuestionDetailContent != value)
                {
                    _MyQuestionDetailContent = value;
                    OnPropertyChanged("MyQuestionDetailContent");
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
