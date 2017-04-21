using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class BirdSurvey
    {
        public int SurveyID { get; set; }
        public DateTime SurveyDate { get; set; }
        public byte SourceID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string SamplePointAreaName { get; set; }
        public byte ClimateID { get; set; }
        public string ClimateName { get; set; }
        public byte SurveyorID { get; set; }
        public string SurveyorName { get; set; }

        public List<BirdSurveyDetails> Details { get; set; }
    }
}
