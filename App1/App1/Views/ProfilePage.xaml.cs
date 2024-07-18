using Android.Graphics.Drawables;
using App1.AuthAndRegModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        User userinfo;
        ToolbarItem edit;
        ToolbarItem exit;
        UserDataController SaveUserData = new UserDataController();
        public ProfilePage(User user)
        {
            InitializeComponent();
            userinfo = user;
            ToolBarButton();
            UserName.Text = user.Name;
            Email.Text = user.Email;
            Role.Text = user.Role;
            RegistrationDate.Text = user.RegistrationDate.ToString();
            RoleCheker(user);
        }

        public void ToolBarButton()
        {
            exit = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,


                IconImageSource = new FileImageSource
                {
                    File = "Exit_Icon"
                }
            };
            

            edit = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,


                IconImageSource = new FileImageSource
                {
                    File = "Edit_Icon"
                }
            };
        }

        public void RoleCheker(User user)
        {
            if (user.Role == "Redaktor")
            {
                ToolbarItems.Add(edit);
                edit.Clicked += Edit_Clicked;
                //Color color = ColorConverters.FromHex("#00214C");


                //Color startColor = Color.FromHex("#0088E6");
                //Color endColor = Color.FromHex("#00214C");

                //PointF startPoint = new PointF(0, 0);
                //PointF endPoint = new PointF(0, 100);

                //LinearGradientBrush gradientBrush = new LinearGradientBrush(
                //    startPoint,
                //    endPoint,
                //    startColor,
                //    endColor,
                //    LinearGradientMode.Vertical);

                //Brush brush = new SolidColorBrush(color);
                //Color color = Color.FromHex("0060AC");
                Button button = new Button
                {
                    Text = "Создать новость",
                    TextColor = Color.White,
                    FontSize = 18,
                    BorderWidth = 1,
                    HeightRequest = 41,
                    WidthRequest = 300,
                    CornerRadius = 150,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Color.FromHex("0060AC")
                //Background = linGrBrush,
            };
                button.Clicked += new EventHandler(Button_Click);
                StackLayoutMain.Children.Add(button);
            }
            ToolbarItems.Add(exit);
            exit.Clicked += Exit_Clicked;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(userinfo));
        }

        private async void Exit_Clicked(object sender, EventArgs e)
        {
            SaveUserData.CleanUserData();
            await Navigation.PopAsync();
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddNewsPage(userinfo), true);
            //await Navigation.PushAsync(new AddNewsPage(), true);
        }

    }
}