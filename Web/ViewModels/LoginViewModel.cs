﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool RememberMe { get; set; }
    }
}
