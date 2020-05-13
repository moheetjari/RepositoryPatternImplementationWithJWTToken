using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicBoot.Api.Entities.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "RoleName is required")]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
