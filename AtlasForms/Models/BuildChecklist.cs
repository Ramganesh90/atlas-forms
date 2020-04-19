using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class BuildChecklist
    {
        [Display(Name = "Pool Code")]
        public int PoolCode { get; set; }

        [Display(Name = "Build For Rack")]
        public int BuildForRack { get; set; }

        [Display(Name = "Stepping Temp")]
        public int SteppingTemp { get; set; }

        [Display(Name = "Fence Direction")]
        public int FenceDirectionID { get; set; }

        [Display(Name = "Fence Install")]
        public int FenceInstallID { get; set; }

        [Display(Name = "Trim In Field")]
        public int TrimInField { get; set; }

        [Display(Name = "Post Pins")]
        public int PostPins { get; set; }

        [Display(Name = "Tear Out Type")]
        public int TearOutTypeID { get; set; }


        [Display(Name = "Dowelled")]
        public int Dowelled { get; set; }

        [Display(Name = "Morticed")]
        public int Morticed { get; set; }

        [Display(Name = "Nail On")]
        public int NailOn { get; set; }

        [Display(Name = "Single Nailed")]
        public int SingleNailed { get; set; }

        [Display(Name = "Build Full Sections")]
        public int BuildFullSections { get; set; }

        [Display(Name = "Stain")]
        public int Stain { get; set; }

        [Display(Name = "Stain Color")]
        public string StainColor { get; set; }
        
        [Display(Name = "StainBrand")]
        public string StainBrand { get; set; }

        [Display(Name = "Safety Officer Onsite")]
        public int SafetyOfficerOnsite { get; set; }

        [Display(Name = "Saftey Meeting/Orintation Onsite")]
        public int SafteyMeetingOrintationOnsiteReq { get; set; }

        [Display(Name = "Saftey Inspection Before Job Starts")]
        public int SafteyInspectionReqBeforeJobStarts { get; set; }

        [Display(Name = "PPE Required")]
        public int PPERequired { get; set; }

        [Display(Name = "Equipment Operator Certs")]
        public int EquipmentOperatorCertsReq { get; set; }

        [Display(Name = "Other Hazards")]
        public string OtherHazards { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public List<DOC10_Responses> ListBuildResponses { get; set; }

        public List<DOC04_FenceDirection> ListFenceDirection { get; set; }

        public List<DOC03_FenceInstall> ListFenceInstall { get; set; }

        public List<DOC06_TearOutTypes> ListTearOutType { get; set; }

    }
}