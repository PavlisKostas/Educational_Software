using Educational_Software.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;



namespace Educational_Software.Navigation_UI_Pages
{
    
    public sealed partial class Edu_3 : Page
    {
        User user;

        public Edu_3()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                user = e.Parameter as User;
            }
        }

        private void Hotspot_1_Enter(object sender, RoutedEventArgs e)
        {
            TeachingTip1.Target = Hotspot_1;
            TeachingTip1.IsOpen = true;
        }

        private void Hotspot_1_Exit(object sender, RoutedEventArgs e)
        {
            TeachingTip1.IsOpen = false;
        }

        private void Hotspot_2_Enter(object sender, RoutedEventArgs e)
        {
            TeachingTip2.Target = Hotspot_2;
            TeachingTip2.IsOpen = true;
        }

        private void Hotspot_2_Exit(object sender, RoutedEventArgs e)
        {
            TeachingTip2.IsOpen = false;
        }

        private void Hotspot_3_Enter(object sender, RoutedEventArgs e)
        {
            TeachingTip3.Target = Hotspot_3;
            TeachingTip3.IsOpen = true;
        }

        private void Hotspot_3_Exit(object sender, RoutedEventArgs e)
        {
            TeachingTip3.IsOpen = false;
        }
    }
}
