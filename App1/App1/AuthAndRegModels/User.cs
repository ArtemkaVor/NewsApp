using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;
using App1.Models;

namespace App1.AuthAndRegModels
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string RegistrationDate { get; set; }

        public int UserID { get; set; }

        private List<User> list = new List<User>();

        private bool Acces;

        private int UserIndex;

        private Dictionary<string, User> data;


        public bool GetAccesToFirebase(string Email, string Password)
        {
            data = JsonConvert.DeserializeObject<Dictionary<string, User>>(DatabaseController.GetBody(@"Users"));
            AccesCheck(data, Email, HashPassword(Password));
            if (Acces)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AccesCheck(Dictionary<string, User> UserData, string Email, string Password)
        {
            try
            {
                foreach (var item in UserData)
                {
                    list.Add(new User { Name = item.Value.Name, Email = item.Value.Email, Password = item.Value.Password, Role = item.Value.Role, RegistrationDate = item.Value.RegistrationDate, UserID = item.Value.UserID });
                }
                var acces = list.FindIndex(x => x.Email == Email && x.Password == Password);
                UserIndex = acces;
                if (acces != -1)
                {
                    Acces = true;
                }
                else
                {
                    Acces = false;
                }
            }
            catch
            {
                Acces = false;
            }
        }

        public User GetUserInfo()
        {
            return list[UserIndex];
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytePassw = Encoding.UTF8.GetBytes(password);
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(sourceBytePassw);
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", String.Empty);
                return hashPassw;
            }
        }
    }
}
