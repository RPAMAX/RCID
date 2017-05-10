using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class PhytoDivision
    {
        public byte DivisionID { get; set; }
        public string DivisionCommonName { get; set; }
        public string DivisionName { get; set; }
        public bool DivisionActive { get; set; }
    }
}
