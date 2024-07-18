using Android.Graphics;
using App1.AuthAndRegModels;
using App1.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewsPage : ContentPage
    {
        User user;
        NewsCategoryChanger categoryChanger = new NewsCategoryChanger();
        public AddNewsPage(User user) // Я тут добавлял Number для News если ошибка то в этом
        {
            InitializeComponent();
            this.user = user;
            CategoryPicker.ItemsSource = categoryChanger.GetCategoryArray();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new
                   RemoteCertificateValidationCallback
                   (
                      delegate { return true; }
                   );
                WebRequest request = WebRequest.Create(EntryImage.Text);
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
                        await DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, попробуйте позже", "Ok");
                        return;
                    }

                }

                if (EntryHeading.Text != "" && EditorDescription.Text != "" && !string.IsNullOrEmpty(CategoryPicker.SelectedItem.ToString()))
                    AddNews();
                else
                    await DisplayAlert("Ошибка", "Поля некоректно заполненны, проверьте и отредактируйте введённые данные", "Ok");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, попробуйте позже", "Ok");
            }
        }

        private void EntryImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            ImageLoad.Source = EntryImage.Text;
        }
        private async void AddNews()
        {
            try
            {
                string Heading = EntryHeading.Text;
                string Description = EditorDescription.Text;
                string Category = CategoryPicker.SelectedItem.ToString();
                string ImageUrl = EntryImage.Text;
                Dictionary<string, Items> data = JsonConvert.DeserializeObject<Dictionary<string, Items>>(DatabaseController.GetBody(@"News"));
                int NewsNumber = data.Count + 1;
                categoryChanger.Met(Category);
                Category = categoryChanger.GetCategoryNewsNumber();
                Items news = new Items()
                {
                    Heading = Heading,
                    Description = Description,
                    Category = Category,
                    Image = ImageUrl,
                    Number = Convert.ToString(NewsNumber),
                    Date = DateTime.Now.ToString("d/M/yyyy"),
                    Watches = 0,
                    OwnerID = user.UserID
                };
                DatabaseController.SetAsync("News/News" + NewsNumber, news); // попробовать вкладку коментарий добавить вторым асюнком
                await DisplayAlert("Успешно", "Ваша новость добавлена", "Ок");
                await Navigation.PopModalAsync();
            }
            catch
            {
                ErorEthernetConnection();
            }
        }
        private async void Button_back(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public async void ErorEthernetConnection()
        {
            await DisplayAlert("Ошибка", "Отсутсвует подключение к интернету", "Ок");
        }
    }
}