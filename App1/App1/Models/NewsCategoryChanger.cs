using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class NewsCategoryChanger
    {
        public string CategoryName;
        public string Category = null;
        private string[] CategoryNumber = new string[10] { "Последние новости", "Новосибирск", "Метро", "Россия", "Шоу бизнес", "Технологии", "Чрезвычайный проишествия", "О самом главном 🔥", "Дорожная ситуация", "Истории читателей" };
        
        public void Met(string CategoryName) // переделывает в число
        {
            NewsCategorySorter newsCategorySorter = new NewsCategorySorter();
            int Number = Array.FindIndex(CategoryNumber, row => row.Contains(CategoryName));
            if(Number != -1)
            {
                newsCategorySorter.CategoryNumber = Number.ToString();
                Category = newsCategorySorter.CategoryNumber;
            }
            else
            {

            }
        }
        public string CategoryNumberConverter(string Category) // преобразовать число в название категории
        {
            Category = CategoryNumber[int.Parse(Category)];
            return Category;
        }

        public string[] GetCategoryArray() // возвращает в массиве все имеющиеся категории 
        {
            return CategoryNumber;
        }

        public string GetCategoryNewsNumber() // возвращает число
        {
            return Category;
        }
    }
}
