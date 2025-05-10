using Educational_Software.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;



namespace Educational_Software.Navigation_UI_Pages
{

    public sealed partial class Edu_2 : Page
    {
        User user;

        public Edu_2()
        {
            this.InitializeComponent();

            
            string videoPath = Path.Combine(Environment.CurrentDirectory, "Assets", "video_v2.mp4");

            
            VideoPlayer.Source = MediaSource.CreateFromUri(new Uri(videoPath));
        }

        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            
            if (VideoPlayer.MediaPlayer != null)
            {
                VideoPlayer.MediaPlayer.Pause();
                VideoPlayer.MediaPlayer.Dispose();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                user = e.Parameter as User;
            }
        }
    }
}
