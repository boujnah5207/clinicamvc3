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
    public partial class Especialidade
    {
        public Especialidade()
        {
            this.FuncionarioEspecialidade = new HashSet<FuncionarioEspecialidade>();
        }
    
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int EspecialidadeId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(100)]
        public string Descricao { get; set; }

        #endregion
    
    
    
    
        public virtual ICollection<FuncionarioEspecialidade> FuncionarioEspecialidade { get; set; }
    }
    
}
