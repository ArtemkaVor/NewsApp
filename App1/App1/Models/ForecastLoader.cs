using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace App1.Models
{
    public class ForecastLoader
    {
       private string TemperatureView;
       private string url;
       private int j = 0;
       private int d = 0;
       private int k = 0;
       public string[] ForecastTemp = new string[9];
       public string[] ForecastDate = new string[5];
       public string[] WeatherName = new string[5];


        public void Loader()
        {
            try
            {
                //BindingContext = this;
                url = "https://api.openweathermap.org/data/2.5/forecast?q=Novosibirsk&units=metric&daily&lang=ru&alerts.description&appid=920a9312496419480af95e2dbae55140";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                for (int i = 2; i <= 39; i++)
                {
                    if (weatherResponse.List[i].dt_txt.Contains("00:00:00") || weatherResponse.List[i].dt_txt.Contains("18:00:00"))
                    {
                        j++;
                        TemperatureView = Math.Round(weatherResponse.List[i].Main.temp).ToString() + "°"; // получние температуры
                        if (j <= 8)
                            ForecastTemp[j] = TemperatureView;
                        if (weatherResponse.List[i].dt_txt.Contains("00:00:00")) // тестовое ускорение цикла
                        {

                            string Date = weatherResponse.List[i].dt_txt.Remove(0, 5).Remove(6, 8).Trim();
                            ForecastDate[d] = Date;
                            WeatherName[k] = weatherResponse.List[i].weather[0].main; // ошибка здесь
                            i = i + 5;
                            d++;
                            k++;
                        }
                        else
                            i++;




                    }

                }
            }
            catch { ForecastTemp = null; ForecastDate = null; WeatherName = null; }
        }
    }
}
