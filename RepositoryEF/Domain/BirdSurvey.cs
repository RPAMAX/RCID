using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class BirdSurvey
    {
        public short SurveyID { get; set; }
        public DateTime SurveyDate { get; set; }
        public short SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public short ClimateID { get; set; }
        public short SurveyorID { get; set; }
    }
}
