using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishSurveyLocation
    {
        public int SurveyID { get; set; }
        public byte SurveyNumber { get; set; }
        public string LocationDetails { get; set; }
        public Nullable<System.DateTime> SurveyDate { get; set; }
        public Nullable<short> SurveyDurationSeconds { get; set; }
        public Nullable<byte> GeneratorID { get; set; }
        public string GeneratorName { get; set; }
        public string SurveyLocationComments { get; set; }
        public bool SurveyLocationActive { get; set; }
    }
}
