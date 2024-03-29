﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class RestPasswordViewModel
    {

        [Required(ErrorMessage = "Required")]
        [RegularExpression(pattern: @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$",
            ErrorMessage = "Enter valid Email!")
            ]
        public string Eamil { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
