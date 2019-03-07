using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Employee 
    {
        [Key]
        public int ID { get; set; }
        //public string Name { get; set; }
        public string Zip { get; set; }
        //public int RoleID { get; set; }
        public string RoleName { get; set; }


        // Add these later, after the AspNet tables have been created, esp. AspNetRoles

        //[ForeignKey("UserRoles")]
        ////[Display(Name = "Team Name")]
        //public int RoleID { get; set; }
        //public Role Role { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        // public string UserType = ApplicationUser.Roles.
        //public IEnumerable<Role> Roles { get; set; }

        //public Employee()
        //{
        //    ApplicationUser.Roles.Where()
        //}


    }
}