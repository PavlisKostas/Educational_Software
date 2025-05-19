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
            //System.Diagnostics.Debug.WriteLine("OI APANTHSEIS EINAI" + user.get_answers().Where(a => a.question == 10).Count());
            var test_2 = user.get_answers().FirstOrDefault(a => a.section == 2 && a.question == 4);
            var test_3 = user.get_answers().FirstOrDefault(a => a.section == 3 && a.question == 4);

            if (user.get_answers().Count(a => a.section == 3) > 0)
            {
                currentUnitValue = 3;
            }
            else
            {
                if (user.get_answers().Count(a => a.section == 2) > 0)
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
                System.Diagnostics.Debug.WriteLine("Oi swstes apanthseis einai" + user.get_answers().FirstOrDefault(a => a.section == 1 && a.question == 4).rating);
                TimeSpan test_1_time = TimeSpan.FromSeconds(test_1.time);
                score_test_1.Text = test_1.rating.ToString();
                test_1_minutes.Text = test_1_time.Minutes.ToString();
                test_1_seconds.Text = test_1_time.Seconds.ToString();
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
                System.Diagnostics.Debug.WriteLine("OI APANTHSEIS EINAI2" + user.get_answers().FirstOrDefault(a => a.section == 2 && a.question == 4).rating);

                TimeSpan test_2_time = TimeSpan.FromSeconds(test_2.time);
                score_test_2.Text = test_2.rating.ToString();
                test_2_minutes.Text = test_2_time.Minutes.ToString();
                test_2_seconds.Text = test_2_time.Seconds.ToString();
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
                System.Diagnostics.Debug.WriteLine("OI APANTHSEIS EINAI2" + user.get_answers().FirstOrDefault(a => a.section == 2 && a.question == 4).rating);
                System.Diagnostics.Debug.WriteLine("OI APANTHSEIS EINAI3" + user.get_answers().FirstOrDefault(a => a.section == 3 && a.question == 4).rating);
                TimeSpan test_3_time = TimeSpan.FromSeconds(test_3.time);
                score_test_3.Text = test_3.rating.ToString();
                test_3_minutes.Text = test_3_time.Minutes.ToString();
                test_3_seconds.Text = test_3_time.Seconds.ToString();
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
