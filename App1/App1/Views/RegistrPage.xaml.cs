using App1.AuthAndRegModels;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using App1.Models;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrPage : ContentPage
    {
        public RegistrPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                if (DisplayNameEntry.Text != "" && EmailEntry.Text.Contains("@") && EmailEntry.Text != "" && PasswordEntry.Text != "" && PasswordEntry.Text == PasswordEntry2.Text)
                {
                    string Date = GenerateTodayDate();
                    Dictionary<string, User> data = JsonConvert.DeserializeObject<Dictionary<string, User>>(DatabaseController.GetBody(@"Users"));
                    int UserNumber = data.Count + 1;
                    User hashuser = new User();
                    string hashpassword = hashuser.HashPassword(PasswordEntry.Text);
                    User user = new User()
                    {
                        Name = DisplayNameEntry.Text,
                        Email = EmailEntry.Text,
                        Password = hashpassword,
                        Role = "User",
                        RegistrationDate = Date.ToString(),
                        UserID = UserNumber
                    };
                    DatabaseController.SetAsync("Users/User" + UserNumber, user);
                    await DisplayAlert("Успешно", "теперь вы можете перейти к авторизаци", "ОК");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Неверно введённые данные", "ОК");
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Неверно введённые данные", "ОК");
            }
        }

        public string GenerateTodayDate()
        {
            int Year = DateTime.Today.Year;
            int Month = DateTime.Today.Month;
            int Day = DateTime.Today.Day;
            DateTime dateValue = new DateTime(Year, Month, Day);
            string Date = dateValue.ToString("d MMMM, yyyy");
            return Date;
        }

    }
}