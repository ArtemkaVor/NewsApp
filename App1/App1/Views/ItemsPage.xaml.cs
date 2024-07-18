using App1.Models;
using App1.ViewModels;
using App1.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1.Views
{
    
    public partial class ItemsPage : ContentPage
    {
        public string TemperatureView;
        string cels = "°";
        DayAndDayOfWeak dayAndDayOfWeak = new DayAndDayOfWeak();
        public ItemsPage()
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
            ToolbarItems.Add(tb);
            InitializeComponent();
            TodayWeather();
            dayAndDayOfWeak.DayOfWeak();
            Forecast();
            
            
            
            
        }
        public void TodayWeather()
        {
            try
            {
                BindingContext = this;
                string url = "https://api.openweathermap.org/data/2.5/weather?q=Novosibirsk&units=metric&daily&lang=ru&alerts.description&appid=920a9312496419480af95e2dbae55140";
                //string url = "https://api.openweathermap.org/data/2.5/forecast?q=Novosibirsk&units=metric&daily&lang=ru&alerts.description&appid=920a9312496419480af95e2dbae55140";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                string TemperatureView = Math.Round(weatherResponse.Main.Temp).ToString(); // получние температуры
                string PressureView = Convert.ToString(Math.Round(weatherResponse.Main.Pressure * 0.75)); // получение давления
                string Feels_like = Math.Round(weatherResponse.Main.Feels_like).ToString(); // получения ощущения температуры
                string Humidity = Convert.ToString(weatherResponse.Main.Humidity); // получение влажности
                string WindSpeed = weatherResponse.Wind.Speed.ToString(); // получение скорости ветра
                string Description = weatherResponse.weather[0].description; // получение информации о погоде
                string Main = weatherResponse.weather[0].main;
                switch (Main)
                {
                    case "Thunderstorm":
                        WeatherImage.Source = "ThunderStorm";
                        break;

                    case "Snow":
                        WeatherImage.Source = "Snow";
                        break;

                    case "Rain":
                        WeatherImage.Source = "RainTest";
                        break;

                    case "Drizzle":
                        WeatherImage.Source = "RainTest";
                        break;

                    case "Clear":
                        WeatherImage.Source = "Clear";
                        break;

                    case "Clouds":
                        WeatherImage.Source = "Clouds";
                        break;
                    default:
                        WeatherImage.Source = "Fog";
                        break;

                } // изображение погодных условий
                #region Устновка значений на UI
                temperaturetxt.Text = TemperatureView + cels;
                fealsliketxt.Text = ($"Ощущается как {Feels_like}°");
                pressuretxt.Text = ($"{PressureView} мм рт. ст.");
                humiditytxt.Text = ($"Влажность {Humidity}%");
                windspeedtxt.Text = ($"Скорость ветра {WindSpeed} м/с");
                SostWeather.Text = ($"{Description}");
                #endregion
            }
            catch
            {

            }
        }
        public async void Forecast()
        {
            try
            {
                ForecastLoader forecastLoader = new ForecastLoader();
                forecastLoader.Loader();
                DateDay1.Text = dayAndDayOfWeak.Day_DayOfWeak[0];
                DateDay2.Text = dayAndDayOfWeak.Day_DayOfWeak[1];
                DateDay3.Text = dayAndDayOfWeak.Day_DayOfWeak[2];
                DateDay4.Text = dayAndDayOfWeak.Day_DayOfWeak[3];
                NightTempDay1.Text = forecastLoader.ForecastTemp[1];
                NightTempDay2.Text = forecastLoader.ForecastTemp[3];
                NightTempDay3.Text = forecastLoader.ForecastTemp[5];
                NightTempDay4.Text = forecastLoader.ForecastTemp[7];
                EveningTempDay1.Text = forecastLoader.ForecastTemp[2];
                EveningTempDay2.Text = forecastLoader.ForecastTemp[4];
                EveningTempDay3.Text = forecastLoader.ForecastTemp[6];
                EveningTempDay4.Text = forecastLoader.ForecastTemp[8];
                WeatherImageForecast1.Source = forecastLoader.WeatherName[0];
                WeatherImageForecast2.Source = forecastLoader.WeatherName[1];
                WeatherImageForecast3.Source = forecastLoader.WeatherName[2];
                WeatherImageForecast4.Source = forecastLoader.WeatherName[3];
            }
            catch
            {
                ErorEthernetConnection();
            }
        }
        public async void ErorEthernetConnection()
        {
            await DisplayAlert("Ошибка", "Отсутсвует подключение к интернету", "Ок");
        }

    }
}