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
    public partial class aspnet_Profile
    {
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public System.Guid UserId { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(1073741823)]
        public string PropertyNames { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(1073741823)]
        public string PropertyValuesString { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public byte[] PropertyValuesBinary { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public System.DateTime LastUpdatedDate { get; set; }

        #endregion
    
    
    
    
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
    
}
