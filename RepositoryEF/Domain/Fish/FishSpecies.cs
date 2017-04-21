using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishSpecies
    {
        public short SpeciesID { get; set; }
        public string SpeciesName { get; set; }
        public Nullable<byte> SpeciesGroupID { get; set; }
        public bool SpeciesActive { get; set; }

    }
}
