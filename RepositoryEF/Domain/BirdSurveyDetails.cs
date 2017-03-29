using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class BirdSurveyDetails
    {
        public short SurveyID { get; set; }
        public short SpeciesID { get; set; }
        public int SurveyDetailCount { get; set; }
    }
}
