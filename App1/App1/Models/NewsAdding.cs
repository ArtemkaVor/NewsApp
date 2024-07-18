using Android.Content.Res;
using App1.AuthAndRegModels;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net;
using System.Text;
using FireSharp;
using System.IO;
using App1.Models;

namespace App1.Models
{
    public class NewsAdding
    {
        public bool Set(Items items)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new
                   RemoteCertificateValidationCallback
                   (
                      delegate { return true; }
                   );
                WebRequest request = WebRequest.Create(items.Image);
                int width;
                int height;
                using (WebResponse response = request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (SKBitmap bitmap = SKBitmap.Decode(stream))
                {
                    // Получаем ширину и высоту изображения
                    width = bitmap.Width;
                    height = bitmap.Height;
                    if (width != 1920 | height != 1080) // доделай валидацию для фото
                    {
                        return false;
                    }

                }

                if (items.Heading != "" && items.Description != "" && !string.IsNullOrEmpty(items.Category))
                {
                    SetNews(items);
                    return true;
                }
                else
                   return false;
            }
            catch
            {
                return false;
            }
        }

        private async void SetNews(Items items)
        {
            NewsCategoryChanger categoryChanger = new NewsCategoryChanger();
            string Heading = items.Heading;
            string Description = items.Description;
            string Category = items.Category;
            string ImageUrl = items.Image;
            string NewsNumber = items.Number;
            categoryChanger.Met(Category);
            Category = categoryChanger.GetCategoryNewsNumber();
            Items news = new Items()
            {
                Heading = Heading,
                Description = Description,
                Category = Category,
                Image = ImageUrl,
                Number = NewsNumber,
                Date = items.Date,
                Watches = items.Watches,
                OwnerID = items.OwnerID
            };
            try
            {
                DatabaseController.SetAsync("News/News" + NewsNumber, news);
                //await client.SetAsync("News/News" + NewsNumber, news); // попробовать вкладку коментарий добавить вторым асюнком
            }
            catch
            {
                
            }
        }

    }
}
