using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class SamplePoint
    {
        public byte SourceID { get; set; }
        public int SamplePointID { get; set; }
        public int ClientLocationID { get; set; }
        public string SamplePointRefID { get; set; }
        public short SamplePointAreaID { get; set; }
        public string SamplePointName { get; set; }
        public Nullable<decimal> SamplePointLatitude { get; set; }
        public Nullable<decimal> SamplePointLongitude { get; set; }
        public Nullable<short> RoleID { get; set; }
        public Nullable<bool> SamplePointAllProcess { get; set; }
        public Nullable<bool> SamplePointPermitted { get; set; }
        public Nullable<bool> SamplePointProcess { get; set; }
        public Nullable<short> SampleTypeID { get; set; }
        public string SamplePointClientSampleName { get; set; }
        public Nullable<short> POTWID { get; set; }
        public Nullable<bool> SamplePointFlowMeters { get; set; }
        public Nullable<bool> SamplePointHazzardWasteDischarge { get; set; }
        public Nullable<bool> SamplePointChemicalFeed { get; set; }
        public Nullable<bool> SamplePointSampleEquipment { get; set; }
        public string SamplePointProcessDiagnosticURL { get; set; }
        public Nullable<bool> SamplePointSurchargeable { get; set; }
        public Nullable<double> SamplePointPercentTotalOfFlow { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedUserID { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<int> EditedUserID { get; set; }
        public bool SamplePointActive { get; set; }
    }
}
