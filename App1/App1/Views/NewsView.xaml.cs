using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.AuthAndRegModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsView : ContentPage
    {
        Items items1;
        bool _isCommentTrue;
        public string Description { get; set; }
        public NewsView(Items items)
        {
            
            InitializeComponent();
            Load();
            items1 = items;
            string CategoryName = items.Category;
            img.Source = items.Image;
            txt.Text = items.Description;
            CategoryView.Text = items.Category;
        }

        private async void Load()
        {
           UserDataController saveUserData = new UserDataController();
           List<string> data = new List<string>();
           data = saveUserData.GetUserData();
            if(data != null)
            {
                UserName.Text = data[2];
                _isCommentTrue = true;
                //Dictionary<string, Comment> CommentData = JsonConvert.DeserializeObject<Dictionary<string, Comment>>(client.Get($@"News/News{items1.Number}/Comments").Body.ToString());
            }
            else
            {
                _isCommentTrue = false;
            }
            try
            {
                Dictionary<string, Comment> Comdata = GetComments();
                if(Comdata.Count == 0) return;
                CommentLoad(Comdata);
            }
            catch
            {
                //await DisplayAlert("Ошибка", "Не удалось загрузить комментарии", "Ок");
                return;
            }
        }

        public void CommentLoad(Dictionary<string, Comment> CommentData)
        {
            commentStack.Children.Clear();
            foreach (var data in CommentData)
            {
                StackLayout mainStackLayout = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    HeightRequest = 130,
                    WidthRequest = 150,
                    Orientation = StackOrientation.Vertical
                };
                commentStack.Children.Add(mainStackLayout);
                StackLayout stackLayoutUser = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };
                mainStackLayout.Children.Add(stackLayoutUser);
                Image image = new Image()
                {
                    Source = "User",
                    WidthRequest = 50,
                    HeightRequest = 50,
                    Margin = new Thickness(10) 
                };
                Label labelUserName = new Label()
                {
                    TextColor = Color.Black,
                    Text = data.Value.UserName
                };
                Label labelDateTimeComment = new Label()
                {
                    Text = data.Value.ComentDate,
                    TextColor = Color.Black
                };
                stackLayoutUser.Children.Add(image);
                stackLayoutUser.Children.Add(labelUserName);
                stackLayoutUser.Children.Add(labelDateTimeComment);
                Label label2 = new Label()
                {
                    Text = data.Value.CommentText,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(10)
                };
                mainStackLayout.Children.Add(label2);
            }
        }
        public Dictionary<string, Comment> GetComments()
        {
            Dictionary<string, Comment> data = JsonConvert.DeserializeObject<Dictionary<string, Comment>>(DatabaseController.GetBody("News/News{items1.Number}/Comments"));
            return data;
        }

        private async void Editor_Completed(object sender, EventArgs e)
        {
            if (_isCommentTrue)
            {

            
            Dictionary<string, Comment> data;
            int CommNumber;
            try
            {
                data = GetComments();
                CommNumber = data.Count + 1;
            }
            catch
            {
                CommNumber = 1;
            }
            try
            {
                if (!string.IsNullOrEmpty(EntryComment.Text))
                {
                    data = GetComments();
                    DateTime now = DateTime.Now;
                    now.ToString("d MMMM, yyyy, HH:mm");
                    Comment comment = new Comment()
                    {
                        CommentText = EntryComment.Text,
                        ComentDate = now.ToString("d MMMM, yyyy, HH:mm"),
                        UserName = Application.Current.Properties["UserName"].ToString()

                    };
                    DatabaseController.SetAsync($"News/News{items1.Number}/Comments/Comment{CommNumber}", comment);
                    await DisplayAlert("Успешно", "Ваш комментарий опубликован", "Ок");
                    Load();
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Ваш комментарий не был опубликован", "Ок");
            }
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы не авторизированы", "Ок");
            }
        }
    }
}