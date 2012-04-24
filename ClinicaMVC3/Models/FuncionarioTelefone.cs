//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaMVC3.Models
{
    public partial class FuncionarioTelefone
    {
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int FuncionarioTelefoneId { get; set; }
    	
        [Required(ErrorMessage="Este campo deve ser preenchido.")]
        [Display(Name="Funcionário")]
        public int FuncionarioId { get; set; }
    	
        [Required(ErrorMessage="Este campo deve ser preenchido.")]
        [Display(Name="Telefone")]
        public int TelefoneId { get; set; }

        #endregion
    
        public virtual Funcionario Funcionario { get; set; }
        public virtual Telefone Telefone { get; set; }
    }
    
}
