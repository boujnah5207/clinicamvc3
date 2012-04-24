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
    public partial class Telefone
    {
        public Telefone()
        {
            this.FuncionarioTelefone = new HashSet<FuncionarioTelefone>();
            this.PacienteTelefone = new HashSet<PacienteTelefone>();
        }
    
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int TelefoneId { get; set; }

    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(15, ErrorMessage = "Este campo aceita no m�ximo 15 car�cteres")]
        [Display(Name="N�mero")]
        [RegularExpression(@"^\(?\d{3}\)?[\s-]?\d{4}-?\d{4}$", ErrorMessage = "Entre com o formato correto: (0xx) 0000-0000")]
        public string Numero { get; set; }

    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int Tipo { get; set; }

        #endregion
    
    
    
    
        public virtual ICollection<FuncionarioTelefone> FuncionarioTelefone { get; set; }
        public virtual ICollection<PacienteTelefone> PacienteTelefone { get; set; }
    }
    
}
