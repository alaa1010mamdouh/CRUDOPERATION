﻿using System.ComponentModel.DataAnnotations;

namespace CRUD.PL.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; } // 

        public bool RememberMe { get; set; }

    }
}
