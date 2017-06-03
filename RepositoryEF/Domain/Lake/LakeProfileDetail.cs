using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class LakeProfileDetail
    {
        public int ProfileID { get; set; }
        public decimal DepthFeet { get; set; }
        public byte ParameterID { get; set; }
        public string ParameterName { get; set; }
        public decimal ParameterValue { get; set; }
        public string ProfileDetailNotes { get; set; }
        public bool ProfileDetailActive { get; set; }
    }
}
