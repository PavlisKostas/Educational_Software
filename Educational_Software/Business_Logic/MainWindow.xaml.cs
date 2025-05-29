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
        

        bool logged_in = false;

        Models.User user = null;


        public MainWindow()
        {
            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hWnd));

            TitleBar_Buttons_Appearance(appWindow.TitleBar);

            GetMonitorDimensions();

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

            user = Models.Server.sign_in(username_sign_in.Text.ToString(), password_sign_in.Password.ToString());

            if (user != null)
            {

                if (user.get_answers().Count(a => a.section == 2 && a.question == 4 && a.userAnswer) > 0)
                {
                    if (user.get_answers().Count(a => a.section == 3 && (a.question == 31 || a.question == 32 || a.question == 33)) > 0)
                    {
                        chapter_1_nav.Visibility = Visibility.Visible;
                        test_1_nav.Visibility = Visibility.Visible;
                        chapter_2_nav.Visibility = Visibility.Visible;
                        test_2_nav.Visibility = Visibility.Visible;
                        chapter_3_nav.Visibility = Visibility.Visible;
                        test_3_nav.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chapter_1_nav.Visibility = Visibility.Visible;
                        test_1_nav.Visibility = Visibility.Visible;
                        chapter_2_nav.Visibility = Visibility.Visible;
                        test_2_nav.Visibility = Visibility.Visible;
                        chapter_3_nav.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (user.get_answers().Count(a => a.section == 1 && a.question == 4 && a.userAnswer) > 0)
                    {
                        if (user.get_answers().Count(a => a.section == 2 && (a.question == 31 || a.question == 32 || a.question == 33)) > 0)
                        {
                            chapter_1_nav.Visibility = Visibility.Visible;
                            test_1_nav.Visibility = Visibility.Visible;
                            chapter_2_nav.Visibility = Visibility.Visible;
                            test_2_nav.Visibility = Visibility.Visible;
                            
                        }
                        else
                        {
                            chapter_1_nav.Visibility = Visibility.Visible;
                            test_1_nav.Visibility = Visibility.Visible;
                            chapter_2_nav.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {

                        if (user.get_answers().Count(a => a.section == 1 && (a.question == 31 || a.question == 32 || a.question == 33)) > 0)
                        {
                            chapter_1_nav.Visibility = Visibility.Visible;
                            test_1_nav.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            chapter_1_nav.Visibility = Visibility.Visible;
                        }

                    }
                }

                welcome_screen.Visibility = Visibility.Collapsed;
                main_screen.Visibility = Visibility.Visible;
                sign_out_button.Visibility = Visibility.Visible;
                logged_in = true;
                Main_TeachingTip.IsOpen = false;
                SignUp_TeachingTip.IsOpen = false;
                SignUpUserError_TeachingTip.IsOpen = false;
            }
            else
            {
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

            chapter_1_nav.Visibility = Visibility.Collapsed;
            chapter_2_nav.Visibility = Visibility.Collapsed;
            chapter_3_nav.Visibility = Visibility.Collapsed;
            test_1_nav.Visibility = Visibility.Collapsed;
            test_2_nav.Visibility = Visibility.Collapsed;
            test_3_nav.Visibility = Visibility.Collapsed;

            Models.Server.sign_out();

        }

        private void Sign_up_semi(object sender, RoutedEventArgs e)
        {
            if(email_obj.Text == "" || name_obj.Text == "" || surname_obj.Text == "" || password_obj.Password == "")
            {
                SignUpUserError_TeachingTip.IsOpen = true;
                return;
            }

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

            user = Models.Server.sign_up(name_obj.Text, surname_obj.Text, email_obj.Text, password_obj.Password);


            Main_TeachingTip.IsOpen = false;
            SignUp_TeachingTip.IsOpen = false;
            SignUpUserError_TeachingTip.IsOpen = false;

            if (user != null)
            {
                if ((bool)sign_up_radio_1_answer3.IsChecked && (bool)sign_up_radio_2_answer2.IsChecked && (bool)sign_up_radio_3_answer1.IsChecked)
                {
                    user.answer(1, 10, 0, 3f, true);
                    user.answer(1, 21, 0, 3f, true);
                    user.answer(1, 31, 0, 3f, true);

                    chapter_1_nav.Visibility = Visibility.Visible;
                    test_1_nav.Visibility = Visibility.Visible;

                }
                else
                {
                    chapter_1_nav.Visibility = Visibility.Visible;
                }


                welcome_screen.Visibility = Visibility.Collapsed;
                main_screen.Visibility = Visibility.Visible;
                sign_out_button.Visibility = Visibility.Visible;
                logged_in = true;
                Main_TeachingTip.IsOpen = false;
                SignUp_TeachingTip.IsOpen = false;
                SignUpUserError_TeachingTip.IsOpen = false;
            }
            else
            {
                email_obj.Text = "";
                password_obj.Password = "";
                name_obj.Text = "";
                surname_obj.Text = "";
                sign_up_2.Visibility = Visibility.Collapsed;
                sign_up_1.Visibility = Visibility.Visible;
                sign_up_radio_1.SelectedItem = null;
                sign_up_radio_2.SelectedItem = null;
                sign_up_radio_3.SelectedItem = null;
                logged_in = false;
                Main_TeachingTip.IsOpen = false;
                SignUpUserError_TeachingTip.IsOpen = false;
                SignUp_TeachingTip.IsOpen = true;
            }
        }

        // Attributes for Windowing - Start (System Configuration DON'T Change) //

        IntPtr hWnd = IntPtr.Zero;
        private SUBCLASSPROC SubClassDelegate;

        int width = 450;
        int height = 400;

        public const int WM_GETMINMAXINFO = 0x0024;

        private const uint MONITOR_SELECTION = 2;

        public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);


        [DllImport("User32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MY_MONITOR_INFO lpmi);


        // Attributes for Windowing - End //


        // Methods for Window Resize - Start (System Configuration DON'T Change) //

        private async Task SizeWindow()
        {

            if (width == 0 || height == 0)
            {
                width = 450;
                height = 400;
                SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
                bool bReturn = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            }
            else
            {

                double dheight = height / 1.35;
                double dwidth = width / 1.2;

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
                    
            }
            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        
        public struct MINMAXINFO
        {
            public System.Drawing.Point ptReserved;
            public System.Drawing.Point ptMaxSize;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Point ptMinTrackSize;
            public System.Drawing.Point ptMaxTrackSize;
        }

        private void GetMonitorDimensions()
        {
            
            IntPtr monitor = MonitorFromWindow(hWnd, MONITOR_SELECTION);

            if (monitor != IntPtr.Zero)
            {
                MY_MONITOR_INFO monitorInfo = new MY_MONITOR_INFO
                {
                    info_Size = Marshal.SizeOf(typeof(MY_MONITOR_INFO))
                };

                if (GetMonitorInfo(monitor, ref monitorInfo))
                {
                    
                    int monitorWidth = monitorInfo.info_Monitor.window_right - monitorInfo.info_Monitor.window_left;
                    int monitorHeight = monitorInfo.info_Monitor.window_bottom - monitorInfo.info_Monitor.window_top;

                    width = monitorWidth;
                    height = monitorHeight;
                    
                    Debug.WriteLine($"<<ALERT>> Monitor Dimensions: {monitorWidth}x{monitorHeight}");
                }
                else
                {
                    width = 0;
                    height = 0;
                }
            }
            else
            {
                width = 0;
                height = 0;
            }
        }

        

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MY_MONITOR_INFO
        {
            public int info_Size;
            public DIMENSIONS info_Monitor;
            public DIMENSIONS info_Work;
            public uint info_Flags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DIMENSIONS
        {
            public int window_left;
            public int window_top;
            public int window_right;
            public int window_bottom;
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
                            contentFrame.Navigate(typeof(Quiz_1), new object[]{ user, this });
                            break;
                        case "test_1":
                            contentFrame.Navigate(typeof(Test_1), new object[] { user, this });
                            break;
                        case "edu_2":
                            contentFrame.Navigate(typeof(Edu_2), user);
                            break;
                        case "quiz_2":
                            contentFrame.Navigate(typeof(Quiz_2), new object[] { user, this });
                            break;
                        case "test_2":
                            contentFrame.Navigate(typeof(Test_2), new object[] { user, this });
                            break;
                        case "edu_3":
                            contentFrame.Navigate(typeof(Edu_3), user);
                            break;
                        case "quiz_3":
                            contentFrame.Navigate(typeof(Quiz_3), new object[] { user, this });
                            break;
                        case "test_3":
                            contentFrame.Navigate(typeof(Test_3), new object[] { user, this });
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
