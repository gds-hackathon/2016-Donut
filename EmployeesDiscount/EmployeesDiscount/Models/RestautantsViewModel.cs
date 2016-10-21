using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesDiscount.Models
{
    public class RestautantsViewModel
    {
            [Required]
            [Display(Name = "RestautantName")]
            [Phone]
            public string RestautantName { get; set; }

            [Required]
            [Display(Name = "Discount")]
            public string Discount { get; set; }

    }
}