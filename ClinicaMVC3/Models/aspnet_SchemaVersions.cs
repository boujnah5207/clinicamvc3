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
    public partial class aspnet_SchemaVersions
    {
    
    
        #region Primitive Properties
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(128)]
        public string Feature { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        [StringLength(128)]
        public string CompatibleSchemaVersion { get; set; }
    	[Required(ErrorMessage="Este campo deve ser preenchido.")]
        public bool IsCurrentVersion { get; set; }

        #endregion
    
    
    
    }
    
}