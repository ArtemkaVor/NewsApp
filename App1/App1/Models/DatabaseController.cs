using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Java.Lang;
using Java.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Android.Content.ClipData;

namespace App1.Models
{
    public static class DatabaseController
    {

        private static IFirebaseClient ConfigLoader()
        {
            IFirebaseClient client;
            IFirebaseConfig ifc = new FirebaseConfig()
            {
                AuthSecret = "pm1VGs0foHOmivbhNVq8FexiqBEPbXbYWfAp59i1",
                BasePath = "https://dbnews54-default-rtdb.europe-west1.firebasedatabase.app/"

            };
            client = new FirebaseClient(ifc);
            return client;
        }

        public async static void SetAsync(string path, object item)
        {
            if(!string.IsNullOrEmpty(path) && item != null)
            {
                IFirebaseClient client = ConfigLoader();
                await client.SetAsync(path, item);
            }
            
        }
        
        public static string GetBody(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                IFirebaseClient client = ConfigLoader();
                var data = client.Get(path).Body;
                return data;
            }
            return null;
        }

    }
}
