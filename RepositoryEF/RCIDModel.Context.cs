﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RCIDRepository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RCID_DWHEntities : DbContext
    {
        public RCID_DWHEntities()
            : base("name=RCID_DWHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bird_Species> Bird_Species { get; set; }
        public virtual DbSet<Bird_Survey> Bird_Survey { get; set; }
        public virtual DbSet<Bird_SurveyDetail> Bird_SurveyDetail { get; set; }
        public virtual DbSet<Bird_Surveyor> Bird_Surveyor { get; set; }
        public virtual DbSet<Lims_SamplePointArea> Lims_SamplePointArea { get; set; }
        public virtual DbSet<Weather_Climate> Weather_Climate { get; set; }
        public virtual DbSet<Fish_Species> Fish_Species { get; set; }
        public virtual DbSet<Fish_SpeciesGroup> Fish_SpeciesGroup { get; set; }
        public virtual DbSet<Fish_Survey> Fish_Survey { get; set; }
        public virtual DbSet<Fish_SurveyDetail> Fish_SurveyDetail { get; set; }
        public virtual DbSet<Fish_SurveyLocation> Fish_SurveyLocation { get; set; }
        public virtual DbSet<Fish_Generator> Fish_Generator { get; set; }
        public virtual DbSet<Phyto_Division> Phyto_Division { get; set; }
        public virtual DbSet<Phyto_Species> Phyto_Species { get; set; }
        public virtual DbSet<Phyto_Survey> Phyto_Survey { get; set; }
        public virtual DbSet<Phyto_SurveyDetail> Phyto_SurveyDetail { get; set; }
        public virtual DbSet<Lake_Parameter> Lake_Parameter { get; set; }
        public virtual DbSet<Lake_Profile> Lake_Profile { get; set; }
        public virtual DbSet<Lake_ProfileDetail> Lake_ProfileDetail { get; set; }
        public virtual DbSet<Lims_SamplePoint> Lims_SamplePoint { get; set; }
    }
}
