using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class SamplePointArea
    {
        public byte SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string SamplePointAreaName { get; set; }
        public bool SamplePointAreaActive { get; set; }
    }
}
