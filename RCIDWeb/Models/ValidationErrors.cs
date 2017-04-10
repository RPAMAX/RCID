using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCIDWeb.Models
{
    public class ValidationErrors
    {
        public List<string> SPAErrors = new List<string>();
        public List<string> SurveyorErrors = new List<string>();
        public List<string> SpeciesErrors = new List<string>();
        public List<string> ClimateErrors = new List<string>();
    }
}