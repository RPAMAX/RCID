//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Fish_SurveyDetail
    {
        public int SurveyID { get; set; }
        public byte SurveyNumber { get; set; }
        public short SurveyDetailID { get; set; }
        public short SpeciesID { get; set; }
        public short SpeciesSizeMillimeters { get; set; }
        public Nullable<decimal> SpeciesSizeInches { get; set; }
        public Nullable<byte> SpeciesSizeInchGroup { get; set; }
        public byte SpeciesWeightPounds { get; set; }
        public decimal SpeciesWeightOunces { get; set; }
        public Nullable<decimal> SpeciesWeightLbs { get; set; }
    
        public virtual Fish_Species Species1 { get; set; }
        public virtual Fish_SurveyLocation SurveyLocation { get; set; }
    }
}
