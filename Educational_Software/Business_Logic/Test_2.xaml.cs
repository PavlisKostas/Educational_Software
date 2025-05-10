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

    public sealed partial class Test_2 : Page
    {
        User user;
        bool isCompleted = false;

        public Test_2()
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

            if (isCompleted)
            {
                ((Button)sender).Content = "Ολοκλήρωση";
                info_message.Severity = InfoBarSeverity.Informational;
                info_message.Title = "Αναμονή ολοκλήρωσης";
                info_message.Message = "Απάντησε τις ερωτήσεις του τεστ σωστά ώστε να μπορέσεις να συνεχίσεις σε επόμενες ενότητες";
                question_1_1_radio.SelectedItem = null;
                NegativeAnswer_checkbox.IsChecked = false;
                PositiveAnswer_checkbox.IsChecked = false;
                question_3_1_combobox.SelectedItem = null;
                question_1_1_radio.IsEnabled = true;
                NegativeAnswer_checkbox.IsEnabled = true;
                PositiveAnswer_checkbox.IsEnabled = true;
                question_3_1_combobox.IsEnabled = true;

                isCompleted = false;
            }
            else
            {
                if (new Random().Next(2) == 0)
                {
                    ((Button)sender).Content = "Επανάληψη";
                    info_message.Severity = InfoBarSeverity.Error;
                    info_message.Title = "Αποτυχία";
                    info_message.Message = "Απαντήσατε σε πολλές ερωτήσεις λάθος. Προσπαθήστε ξανά.";
                    question_1_1_radio.IsEnabled = false;
                    NegativeAnswer_checkbox.IsEnabled = false;
                    PositiveAnswer_checkbox.IsEnabled = false;
                    question_3_1_combobox.IsEnabled = false;

                }
                else
                {
                    ((Button)sender).Content = "Επανάληψη";
                    info_message.Severity = InfoBarSeverity.Success;
                    info_message.Title = "Επιτυχία";
                    info_message.Message = "Συγχαρητήρια! Περάσατε τη δοκιμασία !";
                    question_1_1_radio.IsEnabled = false;
                    NegativeAnswer_checkbox.IsEnabled = false;
                    PositiveAnswer_checkbox.IsEnabled = false;
                    question_3_1_combobox.IsEnabled = false;

                }
                isCompleted = true;
            }

        }
    }
}
