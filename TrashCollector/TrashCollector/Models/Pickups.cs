using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickups
    {
        [Key]
        public int ID { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public DateTime DateOfPickup { get; set; }
        public string Zip { get; set; }
        public int Price { get; set; }
        public bool Paid { get; set; }

    }
}