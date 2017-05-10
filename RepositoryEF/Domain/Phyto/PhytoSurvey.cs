using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class PhytoSurvey
    {
        public int SurveyID { get; set; }
        public DateTime SurveyDate { get; set; }
        public byte SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string LocationDetails { get; set; }
        public bool SurveyActive { get; set; }
    }
}
