using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishSpeciesGroup
    {
        public byte SpeciesGroupID { get; set; }
        public string SpeciesGroupName { get; set; }
        public bool SpeciesGroupActive { get; set; }
    }
}
