﻿using System;
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
    /// PageWriteNote.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageWriteNote : Page
    {
        public static int mId;
        public static string mContent;
        public static bool isInsertUpdate;

        public PageWriteNote()
        {
            InitializeComponent();
            if(isInsertUpdate)  // 새로 삽입
                DataContext = new ViewModelPageWriteNote(this);
            else // 수정
                DataContext = new ViewModelPageWriteNote(this, mId, mContent);
        }
    }
}
