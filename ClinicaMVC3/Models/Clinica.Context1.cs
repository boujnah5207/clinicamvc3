﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ClinicaMVC3.Models
{
    public partial class ClinicaEntities : DbContext
    {
        public ClinicaEntities()
            : base("name=ClinicaEntities")
        {
            /*Database.CreateIfNotExists();*/
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            throw new UnintentionalCodeFirstException();
           
        }        

        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<FuncionarioEspecialidade> FuncionarioEspecialidade { get; set; }
        public DbSet<FuncionarioTelefone> FuncionarioTelefone { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<PacienteTelefone> PacienteTelefone { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        public DbSet<PlanoSaude> PlanoSaude { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
