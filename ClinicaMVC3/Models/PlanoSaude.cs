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
    public partial class PlanoSaude
    {
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int PlanoSaudeId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [MaxLength(100)]

        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }

        #endregion
    
    
    
    }
    
}
