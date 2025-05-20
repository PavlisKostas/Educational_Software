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

    public sealed partial class Test_3 : Page
    {
        User user;
        MainWindow mainWindow;
        bool isCompleted = false;
        bool difficult = true;
        List<bool> answer = new List<bool>();
        DateTime dateTime1;
        List<float> scores = new List<float>();

        public Test_3()
        {
            this.InitializeComponent();
            dateTime1 = DateTime.Now;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is object[] parameters && parameters.Length == 2)
            {
                user = parameters[0] as User;
                mainWindow = parameters[1] as MainWindow;
            }
            float score = user.get_answers().Where(s => s.section == 3).Sum(a => a.rating);
            if (user.get_answers().Count(a => a.section == 3 && a.question == 4 && a.userAnswer) == 0)
            {
                if (score < 2.5f)
                {
                    difficult = false;
                    question_1_1_radio.IsEnabled = false;
                    question_1_1.Visibility = Visibility.Collapsed;

                    question_1_2_radio.IsEnabled = true;
                    question_1_2.Visibility = Visibility.Visible;

                    question_2_1_text.Visibility = Visibility.Collapsed;
                    question_2_2_text.Visibility = Visibility.Visible;

                    question_3_1.Visibility = Visibility.Collapsed;
                    question_3_2.Visibility = Visibility.Visible;
                }
            }
            else
            {
                test_3_questions.Visibility = Visibility.Collapsed;
                test_3_completed.Visibility = Visibility.Visible;
                completion_button.IsEnabled = false;
                completion_button.Visibility = Visibility.Collapsed;
                Bottom_Text.Visibility = Visibility.Collapsed;
                info_message.Severity = InfoBarSeverity.Success;
                info_message.Title = "Επιτυχία";
                info_message.Message = "Έχετε περάσει τη δοκιμασία !";
                question_1_1_radio.IsEnabled = false;
                NegativeAnswer_checkbox.IsEnabled = false;
                PositiveAnswer_checkbox.IsEnabled = false;
                question_3_1_combobox.IsEnabled = false;
            }
        }

        private void PositiveAnswer_checkbox_checked(object sender, RoutedEventArgs e)
        {
            if (NegativeAnswer_checkbox.IsChecked == true)
            {
                NegativeAnswer_checkbox.IsChecked = false;
            }
        }

        private void NegativeAnswer_checkbox_checked(object sender, RoutedEventArgs e)
        {
            if (PositiveAnswer_checkbox.IsChecked == true)
            {
                PositiveAnswer_checkbox.IsChecked = false;
            }
        }

        private void Complete_button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime2 = DateTime.Now;

            if (isCompleted)
            {
                ((Button)sender).Content = "Ολοκλήρωση";
                info_message.Severity = InfoBarSeverity.Informational;
                info_message.Title = "Αναμονή ολοκλήρωσης";
                info_message.Message = "Απάντησε τις ερωτήσεις του τεστ σωστά ώστε να μπορέσεις να συνεχίσεις σε επόμενες ενότητες";
                question_1_1_radio.SelectedItem = null;
                question_1_2_radio.SelectedItem = null;
                NegativeAnswer_checkbox.IsChecked = false;
                PositiveAnswer_checkbox.IsChecked = false;
                question_3_1_combobox.SelectedItem = null;
                question_3_2_combobox.SelectedItem = null;
                question_1_1_radio.IsEnabled = true;
                question_1_2_radio.IsEnabled = true;
                NegativeAnswer_checkbox.IsEnabled = true;
                PositiveAnswer_checkbox.IsEnabled = true;
                question_3_1_combobox.IsEnabled = true;
                question_3_2_combobox.IsEnabled = true;

                isCompleted = false;
            }
            else
            {
                if (difficult)
                {
                    if ((bool)question_1_1_radio_answer1.IsChecked)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }

                    if ((bool)NegativeAnswer_checkbox.IsChecked)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }


                    if (question_3_1_combobox.SelectedIndex == 2)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }
                    

                }
                else
                {
                    if ((bool)question_1_2_radio_answer2.IsChecked)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }

                    if ((bool)PositiveAnswer_checkbox.IsChecked)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }


                    if (question_3_2_combobox.SelectedIndex == 0)
                    {
                        answer.Add(true);
                        scores.Add(1f);
                    }
                    else
                    {
                        answer.Add(false);
                        scores.Add(0f);
                    }
                    
                }

                TimeSpan time_period = dateTime2 - dateTime1;
                int time_period_seconds = (int)time_period.TotalSeconds;

                if (answer.Count(a => a == true) < 2 || time_period_seconds > 300)
                {
                    ((Button)sender).Content = "Επανάληψη";
                    info_message.Severity = InfoBarSeverity.Error;
                    info_message.Title = "Αποτυχία";
                    info_message.Message = "Προσπαθήστε ξανά.";
                    question_1_1_radio.IsEnabled = false;
                    question_1_2_radio.IsEnabled = false;
                    NegativeAnswer_checkbox.IsEnabled = false;
                    PositiveAnswer_checkbox.IsEnabled = false;
                    question_3_1_combobox.IsEnabled = false;
                    question_3_2_combobox.IsEnabled = false;


                    scores.Clear();
                    answer.Clear();

                }
                else
                {
                    ((Button)sender).IsEnabled = false;
                    info_message.Severity = InfoBarSeverity.Success;
                    info_message.Title = "Επιτυχία";
                    info_message.Message = "Συγχαρητήρια! Περάσατε τη δοκιμασία !";
                    question_1_1_radio.IsEnabled = false;
                    question_1_2_radio.IsEnabled = false;
                    NegativeAnswer_checkbox.IsEnabled = false;
                    PositiveAnswer_checkbox.IsEnabled = false;
                    question_3_1_combobox.IsEnabled = false;
                    question_3_2_combobox.IsEnabled = false;
                    user.answer(3, 4, time_period_seconds, scores.Sum(), true);

                }
                isCompleted = true;
            }

        }
    }
}
