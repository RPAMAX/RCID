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
    
    public partial class Lake_Profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lake_Profile()
        {
            this.ProfileDetails = new HashSet<Lake_ProfileDetail>();
        }
    
        public int ProfileID { get; set; }
        public System.DateTime ProfileDate { get; set; }
        public byte SourceID { get; set; }
        public int SamplePointID { get; set; }
        public bool ProfileActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lake_ProfileDetail> ProfileDetails { get; set; }
        public virtual Lims_SamplePoint SamplePoint { get; set; }
    }
}
