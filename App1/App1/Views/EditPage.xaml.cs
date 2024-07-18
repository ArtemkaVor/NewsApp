using App1.AuthAndRegModels;
using App1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        User user;
        NewsAction newsAction = new NewsAction();
        List<Items> NewsList = new List<Items>();
        public EditPage(User user)
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
            ToolbarItems.Add(refresh);
            refresh.Clicked += Refresh_Clicked; ;
            this.user = user;
            AsyncLoad();
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            AsyncLoad();
        }

        public async void AsyncLoad()
        {
            NewsList = newsAction.GetNewsList;
            NewsList = NewsList.Where(x => x.OwnerID == user.UserID).ToList();

            myCollectionView.ItemsSource = NewsList.OrderBy(x => x.Date);
        }

        private async void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemselected = e.CurrentSelection[0] as Items;
            if (itemselected != null)
            {
                newsAction.NewsWathces(itemselected);
                await Application.Current.MainPage.Navigation.PushModalAsync(new EditNewsPage(itemselected));
                myCollectionView.SelectedItem = SelectableItemsView.EmptyViewProperty;
            }
        }
    }
}