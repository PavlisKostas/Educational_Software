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
    
    public sealed partial class Quiz_1 : Page
    {
        User user;
        int current_question_number = 1;
        List<Boolean> question_list = new List<Boolean>();
        DateTime dateTime1;
        int time_delay = 0;
        int answer_timer = 90;

        public Quiz_1()
        {
            this.InitializeComponent();
            dateTime1 = DateTime.Now;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                user = e.Parameter as User;
            }
            List<Answer> answer_list = user.get_answers();
            System.Diagnostics.Debug.WriteLine("OI APANTHSEIS EINAI"+answer_list.Count.ToString());
            foreach (Answer answer in answer_list) 
            {
                System.Diagnostics.Debug.WriteLine(answer.rating);
            }
            if (user.get_answers().Count(a => a.section == 1 && a.question == 10) > 0)
            {
                question_1_radio.IsEnabled = false;
                question_1_radio.Visibility = Visibility.Collapsed;
                question_1_empty.Visibility = Visibility.Visible;
                current_question_number = 2;
                question_1_empty.Visibility = Visibility.Collapsed;
                question_1_answered.Visibility = Visibility.Visible;

                if (user.get_answers().Count(a => a.section == 1 && (a.question == 21 || a.question == 22)) == 0)
                {
                    if (user.get_answers().Count(a => a.section == 1 && a.question == 10 && a.rating == 1f) > 0)
                    {
                        question_2_radio_1.Visibility = Visibility.Visible;
                        question_2_radio_1.IsEnabled = true;
                        question_list.Add(true);
                    }
                    else if (user.get_answers().Count(a => a.section == 1 && a.question == 10 && a.rating == 0.5) > 0)
                    {
                        question_2_radio_2.Visibility = Visibility.Visible;
                        question_2_radio_2.IsEnabled = true;
                        question_list.Add(true);
                        time_delay = 1;
                    }
                    else
                    {
                        question_2_radio_2.Visibility = Visibility.Visible;
                        question_2_radio_2.IsEnabled = true;
                        question_list.Add(false);
                    }
                }
                else if (user.get_answers().Count(a => a.section == 1 && (a.question == 31 || a.question == 32 || a.question == 33)) == 0)
                {

                    question_2_empty.Visibility = Visibility.Collapsed;
                    question_2_answered.Visibility = Visibility.Visible;
                    NextButton.Content = "Ολοκλήρωση";
                    current_question_number = 3;
                    var answer_question = user.get_answers().FirstOrDefault(a => a.question == 21 || a.question == 22);

                    if (answer_question.question == 21 && answer_question.rating == 1f)
                    {
                        question_3_radio_1.Visibility = Visibility.Visible;
                        question_3_radio_1.IsEnabled = true;
                        question_list.Add(true);
                        question_list.Add(true);
                    }
                    else if (answer_question.question == 21 && answer_question.rating == 0.5f)
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        question_list.Add(true);
                        question_list.Add(true);
                        time_delay = 2;
                    }
                    else if (answer_question.question == 21 && answer_question.rating == 0f)
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        question_list.Add(true);
                        question_list.Add(false);
                    }
                    else if (answer_question.question == 22 && answer_question.rating == 1f)
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        var answer_question1 = user.get_answers().FirstOrDefault(a => a.question == 10);
                        if (answer_question1.rating > 0f)
                        {
                            question_list.Add(true);
                        }
                        else
                        {
                            question_list.Add(false);
                        }
                        question_list.Add(true);
                    }
                    else if (answer_question.question == 22 && answer_question.rating == 0.5f)
                    {
                        question_3_radio_3.Visibility = Visibility.Visible;
                        question_3_radio_3.IsEnabled = true;
                        var answer_question1 = user.get_answers().FirstOrDefault(a => a.question == 10);
                        if (answer_question1.rating > 0f)
                        {
                            question_list.Add(true);
                        }
                        else
                        {
                            question_list.Add(false);
                        }
                        question_list.Add(true);
                        time_delay = 2;
                    }
                    else
                    {
                        //current_question_number = 0;
                        question_3_radio_3.Visibility = Visibility.Visible;
                        question_3_radio_3.IsEnabled = true;
                        var answer_question1 = user.get_answers().FirstOrDefault(a => a.question == 10);
                        if (answer_question1.rating > 0f)
                        {
                            question_list.Add(true);
                        }
                        else
                        {
                            question_list.Add(false);
                        }
                        question_list.Add(false);
                    }
                }
                else
                {
                    question_2_empty.Visibility = Visibility.Collapsed;
                    question_2_answered.Visibility = Visibility.Visible;
                    question_3_answered.Visibility = Visibility.Visible;
                    question_3_empty.Visibility = Visibility.Collapsed;
                    NextButton.IsEnabled = false;
                    NextButton.Visibility = Visibility.Collapsed;
                    System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    info_message.Severity = InfoBarSeverity.Success;
                    info_message.Title = "Επιτυχία";
                    info_message.Message = "Συγχαρητήρια! Περάσατε τη δοκιμασία !";
                    //current_question_number = 11;

                    System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
            }
                System.Diagnostics.Debug.WriteLine(question_list.Count);
        }


        private void Next_step(object sender, RoutedEventArgs e)
        {
            DateTime dateTime2 = DateTime.Now;
            TimeSpan time_period = dateTime2 - dateTime1;
            int time_period_seconds = (int)time_period.TotalSeconds;

            if (current_question_number == 1)
            {
                //Check if the answer is correct
                question_1_radio.IsEnabled = false;
                question_2_empty.Visibility = Visibility.Collapsed;
                System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAA"+((bool)question_1_radio_answer1.IsChecked).ToString());

                if ((bool)question_1_radio_answer1.IsChecked && time_period_seconds< answer_timer)
                {
                    question_2_radio_1.Visibility = Visibility.Visible;
                    question_2_radio_1.IsEnabled = true;
                    question_list.Add(true);
                    user.answer(1, 10, time_period_seconds, 1f, true);
                }
                else if((bool)question_1_radio_answer1.IsChecked && time_period_seconds >= answer_timer)
                {
                    question_2_radio_2.Visibility = Visibility.Visible;
                    question_2_radio_2.IsEnabled = true;
                    question_list.Add(true);
                    time_delay++;
                    user.answer(1,10, time_period_seconds, 0.5f, true);
                }
                else
                {
                    question_2_radio_2.Visibility = Visibility.Visible;
                    question_2_radio_2.IsEnabled = true;
                    question_list.Add(false);
                    user.answer(1, 10, time_period_seconds, 0f, false);
                }
                current_question_number++;
                question_number.Text = current_question_number.ToString();
                
            }
            else if (current_question_number == 2)
            {
                //Check if the answer is correct

                question_3_empty.Visibility = Visibility.Collapsed;

                if (question_list[0] == true && time_delay == 0)
                {
                    question_2_radio_1.IsEnabled = false;
                    if ((bool)question_2_radio1_answer3.IsChecked && time_period_seconds< answer_timer)
                    {
                        question_3_radio_1.Visibility = Visibility.Visible;
                        question_3_radio_1.IsEnabled = true;
                        question_list.Add(true);
                        user.answer(1, 21, time_period_seconds, 1f, true);
                    }
                    else if((bool)question_2_radio1_answer3.IsChecked && time_period_seconds >= answer_timer)
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        question_list.Add(true);
                        time_delay++;
                        user.answer(1, 21, time_period_seconds, 0.5f, true);
                    }
                    else
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        question_list.Add(false);
                        user.answer(1, 21, time_period_seconds, 0f, false);
                    }
                }
                else
                {
                    question_2_radio_2.IsEnabled = false;
                    if ((bool)question_2_radio2_answer2.IsChecked && time_period_seconds < answer_timer)
                    {
                        question_3_radio_2.Visibility = Visibility.Visible;
                        question_3_radio_2.IsEnabled = true;
                        question_list.Add(true);
                        user.answer(1, 22, time_period_seconds, 1f, true);
                    }
                    else if ((bool)question_2_radio2_answer2.IsChecked && time_period_seconds >= answer_timer)
                    {
                        question_3_radio_3.Visibility = Visibility.Visible;
                        question_3_radio_3.IsEnabled = true;
                        question_list.Add(true);
                        time_delay++;
                        user.answer(1, 22, time_period_seconds, 0.5f, true);
                    }
                    else
                    {
                        question_3_radio_3.Visibility = Visibility.Visible;
                        question_3_radio_3.IsEnabled = true;
                        question_list.Add(false);
                        user.answer(1, 22, time_period_seconds, 0f, false);
                    }
                }

                ((Button)sender).Content = "Ολοκλήρωση";
                current_question_number++;
                question_number.Text = current_question_number.ToString();

            }
            else if (current_question_number == 3)
            {
                //Check if the answer is correct
                
                if (question_list[0] == true && question_list[1]== true && time_delay == 0)
                {
                    question_3_radio_1.IsEnabled = false;
                    if ((bool)question_3_radio1_answer3.IsChecked && time_period_seconds < answer_timer)
                    {
                        question_list.Add(true);
                        user.answer(1, 31, time_period_seconds, 1f, true);
                    }
                    else if((bool)question_3_radio1_answer3.IsChecked && time_period_seconds >= answer_timer)
                    {
                        question_list.Add(true);
                        time_delay++;
                        user.answer(1, 31, time_period_seconds, 0.5f, true);
                    }
                    else
                    {
                        question_list.Add(false);
                        user.answer(1, 31, time_period_seconds, 0f, false);
                    }
                }
                else if ((question_list[0] == true && question_list[1] == false) || (question_list[0] == false && question_list[1] == true) || (question_list[0] == true && question_list[1] == true && time_delay == 1))
                {
                    question_3_radio_2.IsEnabled = false;
                    if ((bool)question_3_radio2_answer2.IsChecked && time_period_seconds < answer_timer)
                    {
                        question_list.Add(true);
                        user.answer(1, 32, time_period_seconds, 1f, true);
                    }
                    else if((bool)question_3_radio2_answer2.IsChecked && time_period_seconds >= answer_timer)
                    {
                        question_list.Add(true);
                        time_delay++;
                        user.answer(1, 32, time_period_seconds, 0.5f, true);
                    }
                    else
                    {
                        question_list.Add(false);
                        user.answer(1, 32, time_period_seconds, 0f, false);
                    }
                }
                else if (question_list[0] == false && question_list[1] == false || (question_list[0] == true && question_list[1] == true && time_delay == 2))
                {
                    question_3_radio_3.IsEnabled = false;
                    if ((bool)question_3_radio3_answer3.IsChecked && time_period_seconds < answer_timer)
                    {
                        question_list.Add(true);
                        user.answer(1, 33, time_period_seconds, 1f, true);
                    }
                    else if ((bool)question_3_radio3_answer3.IsChecked && time_period_seconds >= answer_timer)
                    {
                        question_list.Add(true);
                        time_delay++;
                        user.answer(1, 33, time_period_seconds, 0.5f, true);
                    }
                    else
                    {
                        question_list.Add(false);
                        user.answer(1, 33, time_period_seconds, 0f, false);
                    }
                }

                question_3_radio_1.IsEnabled = false;
                if (question_list.Count(f=>f==false)>=question_list.Count(t=>t==true))
                {
                    ((Button)sender).Content = "Επανάληψη";
                    info_message.Severity = InfoBarSeverity.Error;
                    info_message.Title = "Αποτυχία";
                    info_message.Message = "Απαντήσατε σε πολλές ερωτήσεις λάθος. Προσπαθήστε ξανά.";
                    current_question_number = 10;
                    user.remove_answer(1);
                }
                else if (question_list.Count(f => f == false) < question_list.Count(t => t == true) && time_delay>=2)
                {
                    ((Button)sender).Content = "Επανάληψη";
                    info_message.Severity = InfoBarSeverity.Error;
                    info_message.Title = "Αποτυχία";
                    info_message.Message = "Ίσως δυσκλοευτήκατε πολύ στις απαντήσεις. Προσπαθήστε ξανά.";
                    current_question_number = 10;
                    //delete answers for quiz 1
                    user.remove_answer(1);
                }
                else
                {
                    ((Button)sender).Content = "Ολοκλήρωση";
                    info_message.Severity = InfoBarSeverity.Success;
                    info_message.Title = "Επιτυχία";
                    info_message.Message = "Συγχαρητήρια! Περάσατε τη δοκιμασία !";
                    current_question_number = 11;
                }
                
                

            }
            else if (current_question_number == 10)
            {
                //Check if the answer is correct
                question_1_empty.Visibility = Visibility.Collapsed;
                question_1_radio.Visibility = Visibility.Visible;
                question_1_radio.SelectedItem = null;
                question_2_radio_1.SelectedItem = null;
                question_2_radio_2.SelectedItem = null;
                question_3_radio_1.SelectedItem = null;
                question_3_radio_2.SelectedItem = null;
                question_3_radio_3.SelectedItem = null;
                question_1_radio.IsEnabled = true;
                question_2_empty.Visibility = Visibility.Visible;
                question_2_radio_1.Visibility = Visibility.Collapsed;
                question_2_radio_2.Visibility = Visibility.Collapsed;
                question_3_empty.Visibility = Visibility.Visible;
                question_3_radio_1.Visibility = Visibility.Collapsed;
                question_3_radio_2.Visibility = Visibility.Collapsed;
                question_3_radio_3.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = "Επόμενη";
                current_question_number = 1;
                question_number.Text = current_question_number.ToString();
                info_message.Severity = InfoBarSeverity.Warning;
                info_message.Title = "Αναμονή Ολοκλήρωσης";
                info_message.Message = "Απάντησε όλες τις ερωτήσεις του Quiz ώστε να ελεγθεί η πρόοδός σου";
            }
            else if (current_question_number == 11)
            {
                //Check if the answer is correct
                //Nothing for now
  
            }

            dateTime1 = DateTime.Now;
        }

        
    }

    
}
