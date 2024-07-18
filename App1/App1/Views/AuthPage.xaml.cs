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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        UserDataController userData = new UserDataController();
        public AuthPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CheckSaveUserData();
        }

        private void CheckSaveUserData()
        {
            
            List<string> accesData = new List<string>();
            accesData = userData.GetUserData();
            if(accesData != null)
            {
                Enter(accesData[0], accesData[1]);
            }
            
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new RegistrPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailEntry.Text) && EmailEntry.Text.Contains("@") && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                string Login = EmailEntry.Text;
                string Password = PasswordEntry.Text;
                Enter(Login, Password);
            }
            else
            {
                await DisplayAlert("Авторизация", "Неверные данные", "OK");
            }
        }

        private async void Enter(string Login, string Password)
        {
            try
            {
                User userAuth = new User();
                bool acces = userAuth.GetAccesToFirebase(Login, Password);
                if (acces)
                {
                    User user = userAuth.GetUserInfo();
                    //await DisplayAlert("Авторизация", acces.ToString(), "OK");
                    userData.SetUserData(Login, Password, user.Name);
                    await Navigation.PushAsync(new ProfilePage(user));
                }
                else
                {

                    await DisplayAlert("Авторизация", acces.ToString(), "OK");
                }
            }
            catch
            {
                ErorEthernetConnection();
            }
            
        }
        public async void ErorEthernetConnection()
        {
            await DisplayAlert("Ошибка", "Отсутсвует подключение к интернету", "Ок");
        }
    }
}