using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer : User
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PickupDayOfWeek { get; set; }
        public DateTime SpecialPickupDate { get; set; }

    }
}