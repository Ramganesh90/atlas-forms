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
        [Required(ErrorMessage = "Pool Code is Required")]
        public int PoolCode { get; set; }

        [Display(Name = "Build For Rack")]
        [Required(ErrorMessage = "Build For Rack is Required")]
        public int BuildForRack { get; set; }

        [Display(Name = "Stepping Temp")]
        [Required(ErrorMessage = "Stepping Temp is Required")]
        public int SteppingTemp { get; set; }

        [Display(Name = "Fence Direction")]
        [Required(ErrorMessage = "Fence Direction is Required")]
        public int FenceDirectionID { get; set; }

        [Display(Name = "Fence Install")]
        [Required(ErrorMessage = "Fence Install is Required")]
        public int FenceInstallID { get; set; }

        [Display(Name = "Trim In Field")]
        [Required(ErrorMessage = "Trim In Field is Required")]
        public int TrimInField { get; set; }

        [Display(Name = "Post Pins")]
        [Required(ErrorMessage = "Post Pins is Required")]
        public int PostPins { get; set; }

        [Display(Name = "Tear Out Type")]
        [Required(ErrorMessage = "Tear Out Type is Required")]
        public int TearOutTypeID { get; set; }


        [Display(Name = "Dowelled")]
        [Required(ErrorMessage = "Dowelled is Required")]
        public int Dowelled { get; set; }

        [Display(Name = "Morticed")]
        [Required(ErrorMessage = "Morticed is Required")]
        public int Morticed { get; set; }

        [Display(Name = "Nail On")]
        [Required(ErrorMessage = "Nail On is Required")]
        public int NailOn { get; set; }

        [Display(Name = "Single Nailed")]
        [Required(ErrorMessage = "Single Nailed is Required")]
        public int SingleNailed { get; set; }

        [Display(Name = "Build Full Sections")]
        [Required(ErrorMessage = "Build Full Sections is Required")]
        public int BuildFullSections { get; set; }

        [Display(Name = "Stain")]
        [Required(ErrorMessage = "Stain is Required")]
        public int Stain { get; set; }

        [Display(Name = "Stain Color")]
        [Required(ErrorMessage = "Stain Color is Required")]
        public string StainColor { get; set; }
        
        [Display(Name = "StainBrand")]
        [Required(ErrorMessage = "Stain Brand is Required")]
        public string StainBrand { get; set; }

        [Display(Name = "Safety Officer Onsite")]
        [Required(ErrorMessage = "Safety Officer Onsite is Required")]
        public int SafetyOfficerOnsite { get; set; }

        [Display(Name = "Saftey Meeting/Orintation Onsite")]
        [Required(ErrorMessage = "Saftey Meeting/Orintation Onsite is Required")]
        public int SafteyMeetingOrintationOnsiteReq { get; set; }

        [Display(Name = "Saftey Inspection Before Job Starts")]
        [Required(ErrorMessage = "Saftey Inspection Before Job Starts is Required")]
        public int SafteyInspectionReqBeforeJobStarts { get; set; }

        [Display(Name = "PPE Required")]
        [Required(ErrorMessage = "PPE is Required")]
        public int PPERequired { get; set; }

        [Display(Name = "Equipment Operator Certs")]
        [Required(ErrorMessage = "Equipment Operator Certs is Required")]
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