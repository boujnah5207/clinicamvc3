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
    public partial class Consulta
    {
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int ConsultaId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int PacienteId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int MedicoId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public System.DateTime Data { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public System.TimeSpan Hora { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public int Status { get; set; }
        #endregion
    
        public virtual Funcionario Funcionario { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
    
}