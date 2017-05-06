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
    
    public partial class Fish_Survey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fish_Survey()
        {
            this.SurveyLocations = new HashSet<Fish_SurveyLocation>();
        }
    
        public int SurveyID { get; set; }
        public short SurveyYear { get; set; }
        public byte SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string SurveyComments { get; set; }
        public bool SurveyActive { get; set; }
    
        public virtual Lims_SamplePointArea SamplePointArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fish_SurveyLocation> SurveyLocations { get; set; }
    }
}
