﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBike.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BikeDBEntities : DbContext
    {
        public BikeDBEntities()
            : base("name=BikeDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bike> Bike { get; set; }
        public virtual DbSet<Hak> Hak { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Marka> Marka { get; set; }
        public virtual DbSet<Tip> Tip { get; set; }
        public virtual DbSet<ResimData> ResimDatas { get; set; }
    }
}
