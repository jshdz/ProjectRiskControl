﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRC.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PRCEntities : DbContext
    {
        public PRCEntities()
            : base("name=PRCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Impacto> Impactos { get; set; }
        public virtual DbSet<Probabilidad> Probabilidades { get; set; }
        public virtual DbSet<Riesgo> Riesgos { get; set; }
    }
}
