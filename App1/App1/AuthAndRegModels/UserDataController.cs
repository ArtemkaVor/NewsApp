using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.AuthAndRegModels
{
    public class UserDataController
    {
        


        public async void SetUserData(string Login, string Password , string UserName)
        {
            Application.Current.Properties["Login"] = Login;
            Application.Current.Properties["Password"] = Password;
            Application.Current.Properties["UserName"] = UserName;
            await Application.Current.SavePropertiesAsync();
        }

        public async void CleanUserData()
        {
            Application.Current.Properties["Login"] = "";
            Application.Current.Properties["Password"] = "";
            Application.Current.Properties["UserName"] = "";
            await Application.Current.SavePropertiesAsync();
        }

        public List<string> GetUserData()
        {
            List<string> data = new List<string>();
            try
            {
                string Login = Application.Current.Properties["Login"].ToString();
                string Password = Application.Current.Properties["Password"].ToString();
                string UserName = Application.Current.Properties["UserName"].ToString();

                if (string.IsNullOrEmpty(Login) & string.IsNullOrEmpty(Password))
                {

                    return null;
                }
                else
                {
                    data.Add(Login);
                    data.Add(Password);
                    data.Add(UserName);
                    return data;

                }
            }
            catch
            {
                return null;
            }
        }

        
    }
}
