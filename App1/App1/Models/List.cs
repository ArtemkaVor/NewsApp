using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class List
    {
        public Main Main { get; set; }

        public Weather[] weather { get; set; }

        public string dt_txt { get; set; }
    }


}
