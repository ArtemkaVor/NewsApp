using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Items
    {

        public string Heading { get; set; }
        public string Image { get; set; }
        public int Watches { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public int OwnerID { get; set; }
        //public Comments Comments = new Comments();

    }
    
}
