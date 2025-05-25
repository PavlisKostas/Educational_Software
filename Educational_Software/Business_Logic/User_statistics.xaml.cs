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
    public sealed partial class User_statistics : Page
    {
        User user;
        int currentUnitValue = 0;

        public User_statistics()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                user = e.Parameter as User;
            }
            user_full_name.Text = user.name + " " + user.lastname;

            var test_1 = user.get_answers().FirstOrDefault(a => a.section == 1 && a.question == 4);
            var test_2 = user.get_answers().FirstOrDefault(a => a.section == 2 && a.question == 4);
            var test_3 = user.get_answers().FirstOrDefault(a => a.section == 3 && a.question == 4);

            if (user.get_answers().Count(a => a.section == 2 && a.question == 4) > 0)
            {
                currentUnitValue = 3;
            }
            else
            {
                if (user.get_answers().Count(a => a.section == 1 && a.question == 4) > 0)
                {
                    currentUnitValue = 2;
                }
                else
                {
                    currentUnitValue = 1;
                }
            }


            sectionProgressBar.Value = currentUnitValue;
            current_unit.Text = currentUnitValue.ToString();

            if (test_1 != null)
            {
                TimeSpan test_1_time = TimeSpan.FromSeconds(test_1.time);
                if(test_1_time.Minutes < 10)
                {
                    test_1_minutes.Text = "0" + test_1_time.Minutes.ToString();
                }
                else
                {
                    test_1_minutes.Text = test_1_time.Minutes.ToString();
                }

                if (test_1_time.Seconds < 10)
                {
                    test_1_seconds.Text = "0" + test_1_time.Seconds.ToString();
                }
                else
                {
                    test_1_seconds.Text = test_1_time.Seconds.ToString();
                }
                score_test_1.Text = test_1.rating.ToString();
                progressBarTest1.Value = test_1.rating;
            }
            else
            {
                score_test_1.Text = "-";
                test_1_minutes.Text = "-";
                test_1_seconds.Text = "-";
                progressBarTest1.Value = 0;
            }

            if (test_2 != null)
            {

                TimeSpan test_2_time = TimeSpan.FromSeconds(test_2.time);
                

                if(test_2_time.Minutes < 10)
                {
                    test_2_minutes.Text = "0" + test_2_time.Minutes.ToString();
                }
                else
                {
                    test_2_minutes.Text = test_2_time.Minutes.ToString();
                }
                if (test_2_time.Seconds < 10)
                {
                    test_2_seconds.Text = "0" + test_2_time.Seconds.ToString();
                }
                else
                {
                    test_2_seconds.Text = test_2_time.Seconds.ToString();
                }
                score_test_2.Text = test_2.rating.ToString();
                progressBarTest2.Value = test_2.rating;
            }
            else
            {
                score_test_2.Text = "-";
                test_2_minutes.Text = "-";
                test_2_seconds.Text = "-";
                progressBarTest2.Value = 0;
            }

            if (test_3 != null)
            {
                TimeSpan test_3_time = TimeSpan.FromSeconds(test_3.time);

                if (test_3_time.Minutes < 10)
                {
                    test_3_minutes.Text = "0" + test_3_time.Minutes.ToString();
                }
                else
                {
                    test_3_minutes.Text = test_3_time.Minutes.ToString();
                }

                if (test_3_time.Seconds < 10)
                {
                    test_3_seconds.Text = "0" + test_3_time.Seconds.ToString();
                }
                else
                {
                    test_3_seconds.Text = test_3_time.Seconds.ToString();
                }

                score_test_3.Text = test_3.rating.ToString();
                progressBarTest3.Value = test_3.rating;
            }
            else
            {
                score_test_3.Text = "-";
                test_3_minutes.Text = "-";
                test_3_seconds.Text = "-";
                progressBarTest3.Value = 0;
            }





        }
    }


}
