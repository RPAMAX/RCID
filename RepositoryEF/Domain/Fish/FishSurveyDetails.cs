﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class FishSurveyDetails
    {
        public int SurveyID { get; set; }
        public byte SurveyNumber { get; set; }
        public short SurveyDetailID { get; set; }
        public short SpeciesID { get; set; }
        public short SpeciesSizeMillimeters { get; set; }
        public Nullable<decimal> SpeciesSizeInches { get; set; }
        public Nullable<byte> SpeciesSizeInchGroup { get; set; }
        public byte SpeciesWeightPounds { get; set; }
        public decimal SpeciesWeightOunces { get; set; }
        public Nullable<decimal> SpeciesWeightLbs { get; set; }
    }
}
