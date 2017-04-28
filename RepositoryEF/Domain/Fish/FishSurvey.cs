using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishSurvey
    {
        public int SurveyID { get; set; }
        public short SurveyYear { get; set; }
        public byte SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string SamplePointAreaName { get; set; }
        public string SurveyComments { get; set; }
    }
}
