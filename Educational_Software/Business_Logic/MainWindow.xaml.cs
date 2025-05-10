using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Educational_Software.Models;
using Educational_Software.Navigation_UI_Pages;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Devices.Display;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinRT;



namespace Educational_Software
{
    
    public sealed partial class MainWindow : Window
    {
        // Attributes for Windowing - Start

        IntPtr hWnd = IntPtr.Zero;
        private SUBCLASSPROC SubClassDelegate;

        int width = 450;
        int height = 400;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref bool attrValue, int attrSize);

        // Attributes for Windowing - End

        bool logged_in = false;

        Models.User user = null;


        public MainWindow()
        {
            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hWnd));

            TitleBar_Buttons_Appearance(appWindow.TitleBar);


            SizeWindow();

            this.InitializeComponent();
            this.SystemBackdrop = new MicaBackdrop();
            ExtendsContentIntoTitleBar = true;

            AppWindow.Title = "Educational Software";
            AppWindow.SetIcon("Assets/light_house.ico");

            System.Diagnostics.Debug.WriteLine("going to create tables");

            DatabaseHandler.CreateTables();


        }

        private void Log_in(object sender, RoutedEventArgs e)
        {
           

            Debug.WriteLine("going to sign in");

            Debug.WriteLine(email_obj.Text);

            user = Models.Server.sign_in(username_sign_in.Text.ToString(), password_sign_in.Password.ToString());

            if (user != null)
            {
                Debug.WriteLine("successful sign in");
                welcome_screen.Visibility = Visibility.Collapsed;
                main_screen.Visibility = Visibility.Visible;
                sign_out_button.Visibility = Visibility.Visible;
                logged_in = true;
                Main_TeachingTip.IsOpen = false;
            }
            else
            {
                Debug.WriteLine("unsuccessful sign in");
                logged_in = false;
                Main_TeachingTip.IsOpen = true;
            }

        }

        private void Sign_out(object sender, RoutedEventArgs e)
        {

            chapter_1_nav.IsExpanded = false;
            chapter_2_nav.IsExpanded = false;
            chapter_3_nav.IsExpanded = false;

            sign_up_2.Visibility = Visibility.Collapsed;
            sign_up_1.Visibility = Visibility.Visible;
            sign_up_radio_1.SelectedItem = null;
            sign_up_radio_2.SelectedItem = null;
            sign_up_radio_3.SelectedItem = null;

            email_obj.Text = "";
            name_obj.Text = "";
            surname_obj.Text = "";
            password_obj.Password = "";

            username_sign_in.Text = "";
            password_sign_in.Password = "";

            sign_out_button.Visibility = Visibility.Collapsed;
            main_screen.Visibility = Visibility.Collapsed;
            welcome_screen.Visibility = Visibility.Visible;

            logged_in = false;

            navigation_element.SelectedItem = navigation_element.MenuItems[0];
            
        }

        private void Sign_up_semi(object sender, RoutedEventArgs e)
        {
            sign_up_1.Visibility = Visibility.Collapsed;
            sign_up_2.Visibility = Visibility.Visible;

        }

        private void Sign_up_back(object sender, RoutedEventArgs e)
        {
            sign_up_2.Visibility = Visibility.Collapsed;
            sign_up_1.Visibility = Visibility.Visible;
            sign_up_radio_1.SelectedItem = null;
            sign_up_radio_2.SelectedItem = null;
            sign_up_radio_3.SelectedItem = null;
            
        }

        private void Sign_up(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("going to sign up");

            user = Models.Server.sign_up(name_obj.Text, surname_obj.Text, email_obj.Text, password_obj.Password);

            Debug.WriteLine("successful signup going to sign in");

            Main_TeachingTip.IsOpen = false;
        }


        // Methods for Window Resize - Start //

        private async Task SizeWindow()
        {
            var monitorInfo = default(DisplayMonitor);
            var displayList = await DeviceInformation.FindAllAsync
                              (DisplayMonitor.GetDeviceSelector());

            if (!displayList.Any())
                return;

            if (displayList.Count > 1)
            {

                monitorInfo = await DisplayMonitor.FromInterfaceIdAsync(displayList[1].Id);
                Debug.WriteLine("<<ALERT>> Monitor 1: " + monitorInfo.NativeResolutionInRawPixels.Width + "x" + monitorInfo.NativeResolutionInRawPixels.Height);
            }
            else
            {

                monitorInfo = await DisplayMonitor.FromInterfaceIdAsync(displayList[0].Id);
                Debug.WriteLine("<<ALERT>> Monitor 0: " + monitorInfo.NativeResolutionInRawPixels.Width + "x" + monitorInfo.NativeResolutionInRawPixels.Height);
            }



            if (monitorInfo == null)
            {
                width = 450;
                height = 400;
                SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
                bool bReturn = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            }
            else
            {
                double dheight = monitorInfo.NativeResolutionInRawPixels.Height / 1.5;
                double dwidth = monitorInfo.NativeResolutionInRawPixels.Width / 1.4;

                height = (int)dheight;
                width = (int)dwidth;
                SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
                bool bReturn = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            }
        }


        private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
        {
            switch (uMsg)
            {
                case WM_GETMINMAXINFO:
                    {
                        MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
                        mmi.ptMinTrackSize.X = width;
                        mmi.ptMinTrackSize.Y = height;
                        Marshal.StructureToPtr(mmi, lParam, false);
                        return 0;
                    }
                    break;
            }
            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        public const int WM_GETMINMAXINFO = 0x0024;

        public struct MINMAXINFO
        {
            public System.Drawing.Point ptReserved;
            public System.Drawing.Point ptMaxSize;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Point ptMinTrackSize;
            public System.Drawing.Point ptMaxTrackSize;
        }

        // Methods for Window Resize - End //

        //TitleBar customization
        private void TitleBar_Buttons_Appearance(AppWindowTitleBar titleBar)
        {
            titleBar.ButtonHoverBackgroundColor = Colors.LightGray;
            titleBar.ButtonPressedBackgroundColor = Colors.LightGray;

            // Set button foreground colors
            titleBar.ButtonForegroundColor = Colors.Black;
            titleBar.ButtonHoverForegroundColor = Colors.Black;
            titleBar.ButtonPressedForegroundColor = Colors.Black;
        }

        private void nv_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //Nothing to do here
            }
            else if (args.SelectedItem != null)
            {

                NavigationViewItem item = args.SelectedItem as NavigationViewItem;


                if (item != null && item.Tag != null)
                {
                    string selectedTag = item.Tag.ToString();
                    switch (selectedTag)
                    {
                        case "start":
                            if (logged_in)
                            {
                                contentFrame.Navigate(typeof(Home), null);
                            }
                            else
                            {
                                contentFrame.Navigate(typeof(Home), null, new Microsoft.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
                            }
                            break;
                        case "edu_1":
                            contentFrame.Navigate(typeof(Edu_1), user);
                            break;
                        case "quiz_1":
                            contentFrame.Navigate(typeof(Quiz_1), user);
                            break;
                        case "test_1":
                            contentFrame.Navigate(typeof(Test_1), user);
                            break;
                        case "edu_2":
                            contentFrame.Navigate(typeof(Edu_2), user);
                            break;
                        case "quiz_2":
                            contentFrame.Navigate(typeof(Quiz_2), user);
                            break;
                        case "test_2":
                            contentFrame.Navigate(typeof(Test_2), user);
                            break;
                        case "edu_3":
                            contentFrame.Navigate(typeof(Edu_3), user);
                            break;
                        case "quiz_3":
                            contentFrame.Navigate(typeof(Quiz_3), user);
                            break;
                        case "test_3":
                            contentFrame.Navigate(typeof(Test_3), user);
                            break;
                        case "user_statistics":
                            contentFrame.Navigate(typeof(User_statistics), user);
                            break;
                        case "about_the_app":
                            contentFrame.Navigate(typeof(About_the_app));
                            break;

                    }
                }
            }
        }
    }
}
