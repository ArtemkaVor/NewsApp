using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValueView : ContentPage
    {
        string KursName;
        string content;
        public ValueView()
        {
            ToolbarItem tb = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,


                IconImageSource = new FileImageSource
                {
                    File = "NewsLogoWhiteBigForIcon"
                }
            };
            try
            {
                ToolbarItems.Add(tb);
                tb.Clicked += KursPicker;
                InitializeComponent();
                BindingContext = this;
                var client = new WebClient();
                ServicePointManager.ServerCertificateValidationCallback = new
                    RemoteCertificateValidationCallback
                    (
                       delegate { return true; }
                    );
                content = client.DownloadString("https://www.cbr-xml-daily.ru/daily.xml");
                XDocument xdoc = XDocument.Parse(content);
                var el = xdoc.Element("ValCurs").Elements("Valute");
                string dollar = el.Where(x => x.Attribute("ID").Value == "R01235").Select(x => x.Element("Value").Value).FirstOrDefault();
                string eur = el.Where(x => x.Attribute("ID").Value == "R01239").Select(x => x.Element("Value").Value).FirstOrDefault();
                dollar = dollar.Remove(dollar.Length - 2);
                eur = eur.Remove(eur.Length - 2);
                usdtxt.Text = ($"$ = {dollar}₽");
                eurtxt.Text = ($"€ = {eur}₽");
            }
            catch
            {
                ErorEthernetConnection();
            }
            
        }
        public async void KursPicker(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Parse(content);
            string[] Name = { "Швейцарский франк", "Фунт стерлингов", "Японская йена","Белорусский рубль", "Турецкая лира", "Китайский юань", "Польский злотый"}; 
            KursName = await DisplayActionSheet("Выберите валюту", "", "", "Швейцарский франк", "Фунт стерлингов", "Японская йена","Белорусский рубль", "Турецкая лира", "Китайский юань", "Польский злотый");
            if(KursName != null)
            {
                string[] KursCode = { "R01775", "R01035", "R01820", "R01335", "R01090B", "R01700J", "R01375", "R01565" };
                int Number = Array.FindIndex(Name, row => row.Contains(KursName));
                var el = xdoc.Element("ValCurs").Elements("Valute");
                string Value = el.Where(x => x.Attribute("ID").Value == KursCode[Number]).Select(x => x.Element("Value").Value).FirstOrDefault();
                Value = Value.Remove(Value.Length - 2);
                UserKurseName.Text = Name[Number].ToString();
                UserKursChange.Text = Value.ToString() + "₽";
            }
            
        }

        public async void ErorEthernetConnection()
        {
            await DisplayAlert("Ошибка", "Отсутсвует подключение к интернету", "Ок");
        }
    }
}