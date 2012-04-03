using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicaMVC3.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e confirmação de senha não correspondem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me??")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo deve ser preenchido.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da senha")]
        [Compare("Password", ErrorMessage = "A nova senha e confirmação de senha não correspondem.")]
        public string ConfirmPassword { get; set; }
    }
}
