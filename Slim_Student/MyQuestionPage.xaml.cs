﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Slim_professor
{
    public partial class SubjectStatsPage
    {
        public SubjectStatsPage()
        {
            this.InitializeComponent();


        }

        private void SignalLightMonitorBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SignalLightMonitorPage.xaml", UriKind.Relative));
        }

        private void HiddenTalkBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("HiddenTalkPage.xaml", UriKind.Relative));
        }

        private void QnABtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("QnAPage.xaml", UriKind.Relative));
        }

        private void NoticeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("NoticePage.xaml", UriKind.Relative));
        }

    }
}