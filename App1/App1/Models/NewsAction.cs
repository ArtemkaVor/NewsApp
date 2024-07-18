using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;

namespace App1.Models
{
    public class NewsAction
    {

        public NewsAction()
        {

        }

        public List<Items> GetNewsList
        {
            get
            {
                try { 
                List<Items> NewsList = new List<Items>();
                NewsCategoryChanger NewsCategoryChanger = new NewsCategoryChanger();
                Dictionary<string, Items> data = JsonConvert.DeserializeObject<Dictionary<string, Items>>(DatabaseController.GetBody("News"));
                foreach (var items in data)
                {
                    if (!string.IsNullOrEmpty(items.Value.Heading) & !string.IsNullOrEmpty(items.Value.Description) & !string.IsNullOrEmpty(items.Value.Image))
                    {
                        items.Value.Category = NewsCategoryChanger.CategoryNumberConverter(items.Value.Category);
                        NewsList.Add(items.Value);
                    }

                }

                return NewsList;
                }
                catch { return null; }
            }
        }

        public void NewsWathces(Items changedNews)
        {
            DatabaseController.SetAsync("News/News" + changedNews.Number + "/Watches", changedNews.Watches + 1);
        }
    }

}
