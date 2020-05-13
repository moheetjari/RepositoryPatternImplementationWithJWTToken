using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicBoot.Api.Entities.Models
{
    [Table("testtb")]
    public class Employee
    {
        [Key]
        public int empId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string empName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string empAddress { get; set; }

        [Required(ErrorMessage = "Mobileno is required")]
        public string empMobileno { get; set; }
    }
}
