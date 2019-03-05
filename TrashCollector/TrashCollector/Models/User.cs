using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserRoles")]
        //[Display(Name = "Team Name")]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<Role> Roles { get; set; }



    }
}