﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EBeats.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password aktual")]
        public string? OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password i ri")]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo Password")]
        [Compare("NewPassword", ErrorMessage = "Passwordet nuk përputhen!")]
        public string? ConfirmPassword { get; set; }
    }
}
