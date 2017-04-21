using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class BirdSurveyor
    {
        public byte SurveyorID { get; set; }
        public string SurveyorName { get; set; }
        public bool SurveyorActive { get; set; }
    }
}
