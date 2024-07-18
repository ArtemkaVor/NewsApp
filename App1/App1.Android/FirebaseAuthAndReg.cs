using Android.Gms.Extensions;
using Android.Gms.Tasks;
using App1.AuthAndRegModels;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthAndReg))] // тут я фигню добавил (Xamarin.Forms.) мб тут ошибка
namespace App1.AuthAndRegModels
{
    public class FirebaseAuthAndReg : IFirebaseInterfaceAuth
    {
        public async Task<string> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var getTokenResult = await authResult.User.GetIdToken(false);
            return Convert.ToString(getTokenResult);
            //return getTokenResult.Token;
        }
        public async Task<string> CreateUserWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
            //var getTokenResult = await authResult.User.GetIdToken(false);
            //return getTokenResult.Token;
            return null;
        }
    }
}
