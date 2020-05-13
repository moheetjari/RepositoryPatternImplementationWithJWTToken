using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicBoot.Api.Entities.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
