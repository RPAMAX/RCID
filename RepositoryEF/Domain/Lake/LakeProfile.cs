using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class LakeProfile
    {
        public int ProfileID { get; set; }
        public System.DateTime ProfileDate { get; set; }
        public byte SourceID { get; set; }
        public int SamplePointID { get; set; }
        public string SamplePointName { get; set; }
    }
}
