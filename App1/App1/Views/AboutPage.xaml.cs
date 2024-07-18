using App1.Models;
using App1.ViewModels;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using App1.AuthAndRegModels;
using FireSharp.Config;
using FireSharp.Interfaces;
using static Android.Util.EventLogTags;
using System.Linq;

namespace App1.Views
{
    public partial class AboutPage : ContentPage
    {
        
        ObservableCollection<Items> items;
        NewsCategorySorter NewsCategorySorter = new NewsCategorySorter();
        NewsCategoryChanger NewsCategoryChanger = new NewsCategoryChanger();
        List<Items> NewsList = new List<Items>();
        NewsAction newsAction = new NewsAction();
        string CategoryName;
        public AboutPage()
        {
            InitializeComponent();
            ToolbarItem refresh = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,


                IconImageSource = new FileImageSource
                {
                    File = "Refresh_Icon"
                }
            };
            ToolbarItem tb = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                

                IconImageSource = new FileImageSource
                {
                    File = "Category_Icon"
                }
            };
            ToolbarItem SortButon = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,


                IconImageSource = new FileImageSource
                {
                    File = "Sort_Ico"
                }
            };
                AsyncLoad();
            ToolbarItems.Add(refresh);
            ToolbarItems.Add(tb);
            ToolbarItems.Add(SortButon);
            refresh.Clicked += Refresh_Clicked;
            tb.Clicked += FiltrButton_Clicked;
            SortButon.Clicked += SortButon_Clicked;
            //AboutPage aboutPage = new AboutPage(); так не делать

        }

        public async void ErorEthernetConnection()
        {
            await DisplayAlert("Ошибка", "Отсутсвует подключение к интернету", "Ок");
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            AsyncLoad();
        }

        private async void SortButon_Clicked(object sender, EventArgs e)
        {
            string SortName = await DisplayActionSheet("Отсортировать список", "", "", "Последние новости", "Устаревшие новости");
            switch (SortName)
            {
                case "Последние новости":
                    NewsList = NewsList.OrderBy(x => x.Date).ToList();
                    myCollectionView.ItemsSource = NewsList;
                    break;
                case "Устаревшие новости":
                    NewsList = NewsList.OrderByDescending(x => x.Date).ToList();
                    myCollectionView.ItemsSource = NewsList;
                    break;
            }
            
        }

        private async void FiltrButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //string CategoryNumber = await DisplayPromptAsync("Выберите категорию", "номер от 1 до 4", maxLength: 1, keyboard: Keyboard.Numeric);
            //NewsCategorySorter.CategoryNumber = CategoryNumber;
            //NewsCategorySorter.SortetActivated = true;
            CategoryName = await DisplayActionSheet("Выберите категорию", "", "", "Последние новости", "Новосибирск", "Метро", "Россия", "Шоу бизнес", "Технологии", "Чрезвычайный проишествия", "О самом главном 🔥", "Дорожная ситуация", "Истории читателей");
            if(CategoryName != null)
            {
                //NewsCategoryChanger.CategoryName = CategoryName;
                NewsCategoryChanger.Met(CategoryName);
                if(NewsCategoryChanger.Category != "0")
                NewsCategorySorter.SortetActivated = true;
                else
                    NewsCategorySorter.SortetActivated = false;
                AsyncLoad();
                
            }
            
            


        }
        
        public async void AsyncLoad()
        {
            using (var firebase = new FirebaseClient("https://dbnews54-default-rtdb.europe-west1.firebasedatabase.app/"))
            {
                
                items = new ObservableCollection<Items>
                {

                };
                items.CollectionChanged += Items_CollectionChanged; // подписка за отслеживанием коллекции
                
                NewsList = newsAction.GetNewsList;
                if (NewsList != null)
                {
                    if (NewsCategorySorter.SortetActivated)
                        NewsList = NewsList.Where(x => x.Category.Contains(CategoryName)).ToList();

                    myCollectionView.ItemsSource = NewsList.OrderBy(x => x.Date);
                }
                else
                {
                    ErorEthernetConnection();
                }
            }
        }



        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) // отслеживание коллекции
        {
            
            
        }

        private async void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemselected = e.CurrentSelection[0] as Items;
            if (itemselected != null)
            {
                newsAction.NewsWathces(itemselected);
                await Navigation.PushAsync(new NewsView(itemselected));
                myCollectionView.SelectedItem = SelectableItemsView.EmptyViewProperty;
            }
        }

    }

       
}
