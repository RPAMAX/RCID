using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class LakeParameter
    {
        public byte ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string ParameterFullName { get; set; }
        public string ParameterTestMethod { get; set; }
        public string ParameterUnit { get; set; }
        public bool ParameterActive { get; set; }
    }
}
