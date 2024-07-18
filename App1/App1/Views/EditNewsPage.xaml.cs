using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNewsPage : ContentPage
    {
        Items items;
        NewsCategoryChanger categoryChanger = new NewsCategoryChanger();
        public EditNewsPage(Items items)
        {
            InitializeComponent();
            this.items = items;
            CategoryPicker.ItemsSource = categoryChanger.GetCategoryArray();
            DataLoad();
        }

        public void DataLoad()
        {
            EntryHeading.Text = items.Heading;
            EditorDescription.Text = items.Description;
            EntryImage.Text = items.Image;
            CategoryPicker.SelectedItem = items.Category;
        }
        private void EntryImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            ImageLoad.Source = EntryImage.Text;
        }

        public async void Set()
        {
            Items items = new Items()
            {
                Heading = EntryHeading.Text,
                Description = EditorDescription.Text,
                Image = EntryImage.Text,
                Category = CategoryPicker.SelectedItem.ToString(),
                Number = this.items.Number,
                Date = this.items.Date,
                OwnerID = this.items.OwnerID,
                Watches = this.items.Watches,

            };
            Models.NewsAdding newsAdding = new Models.NewsAdding();
            var result = newsAdding.Set(items);
            if (result) await DisplayAlert("Успешно", "Ваша новость опубликована", "Ок");
            else
            {
                await DisplayAlert("Ошибка", "Ваша новость не была изменина, возможно вы некоректно указали данные", "Ок");
                return;
            }
            await Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Set();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}