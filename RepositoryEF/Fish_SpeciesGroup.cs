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
    
    public partial class Fish_SpeciesGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fish_SpeciesGroup()
        {
            this.Species = new HashSet<Fish_Species>();
        }
    
        public byte SpeciesGroupID { get; set; }
        public string SpeciesGroupName { get; set; }
        public bool SpeciesGroupActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fish_Species> Species { get; set; }
    }
}