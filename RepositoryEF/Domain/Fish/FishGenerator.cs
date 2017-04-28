using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishGenerator
    {
        public byte GeneratorID { get; set; }
        public string GeneratorName { get; set; }
        public bool GeneratorActive { get; set; }
    }
}
