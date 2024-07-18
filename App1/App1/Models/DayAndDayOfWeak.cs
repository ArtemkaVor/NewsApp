using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class DayAndDayOfWeak
    {
        public string[] Day_DayOfWeak = new string[5];

        public void DayOfWeak()
        {
            //string[] Day_DayOfWeakRus = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
            int Year = DateTime.Today.Year;
            int Month = DateTime.Today.Month;
            int Day = DateTime.Today.Day;
            DateTime dateValue = new DateTime(Year, Month, Day);
            for (int i = 0; i <= 4; i++)
            {
                dateValue = dateValue.AddDays(1);
                Day_DayOfWeak[i] = dateValue.ToString($"{"ddd"} {"d"}");
                //switch (dateValue.ToString($"{"ddd"}"))
                //{
                //    case "Mon":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[1]} {"d"}")); ;
                //        break;
                //    case "Tue":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[2]} {"d"}")); ;
                //        break;
                //    case "Wed":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[3]} {"d"}")); ;
                //        break;
                //    case "Thu":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[4]} {"d"}")); ;
                //        break;
                //    case "Fri":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[5]} {"d"}")); ;
                //        break;
                //    case "Sat":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[6]} {"d"}")); ;
                //        break;
                //    case "Sun":
                //        Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[7]} {"d"}")); ;
                //        break;

                //}
                //if(dateValue.ToString($"{"ddd"}").Contains("Mon"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[1]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Tue"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[2]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Wed"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[3]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Thu"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[4]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Fri"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[5]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Sat"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[6]} {"d"}")); ;
                //if (dateValue.ToString($"{"ddd"}").Contains("Sun"))
                //    Day_DayOfWeak[i] = (dateValue.ToString($"{Day_DayOfWeakRus[7]} {"d"}")); ;
            }
        }
    }
}

