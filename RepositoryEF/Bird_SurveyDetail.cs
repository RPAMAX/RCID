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
    
    public partial class Bird_SurveyDetail
    {
        public int SurveyID { get; set; }
        public short SpeciesID { get; set; }
        public int SurveyDetailCount { get; set; }
        public bool SurveyDetailActive { get; set; }
    
        public virtual Bird_Species Species { get; set; }
        public virtual Bird_Survey Survey { get; set; }
    }
}
