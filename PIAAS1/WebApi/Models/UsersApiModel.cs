using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class UsersApiModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string role { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string img { get; set; }
        public string gender { get; set; }

        public string country { get; set; }

        public string Address { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime birthday { get; set; }

    }
}