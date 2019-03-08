using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer 
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? DayOfWeekPickup { get; set; }
        public DateTime? SpecialPickupDate { get; set; }
        public DateTime? StopDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int Bill { get; set; }
        public string BillDetails { get; set; }
        public string RoleName { get; set; }


        // Add these later, after the AspNet tables have been created, esp. AspNetRoles

        //[ForeignKey("UserRoles")]
        ////[Display(Name = "Team Name")]
        //public int RoleID { get; set; }
        ////public Role Role { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //public IEnumerable<Role> Roles { get; set; }

    }
}