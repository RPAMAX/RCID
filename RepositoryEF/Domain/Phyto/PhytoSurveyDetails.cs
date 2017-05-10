using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class PhytoSurveyDetails
    {
        public int SurveyID { get; set; }
        public short SpeciesID { get; set; }
        public string SpeciesName { get; set; }
        public decimal SurveyCount { get; set; }
        public bool SurveyDetailActive { get; set; }
    }
}
