using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class Installation
    {
        [Display(Name = "CBYD")]
        [Required(ErrorMessage = "CBYD is Required")]
        public int CBYD { get; set; }

        [Display(Name = "CBYD Date")]
        public string CBYDDate { get; set; }

        [Display(Name = "CBYD Number")]
        public string CBYDNumber { get; set; }

        [Display(Name = "Equipment Required")]
        [Required(ErrorMessage = "Equipment is Required")]
        public string EquipmentRequired { get; set; }

        [Display(Name = "DigTypeID")]
        [Required(ErrorMessage = "DigType is Required")]
        public int DigTypeID { get; set; }

        [Display(Name = "Water Available")]
        [Required(ErrorMessage = "Water Available is Required")]
        public int WaterAvailible { get; set; }

        [Display(Name = "Electricity Available")]
        [Required(ErrorMessage = "Electricity Available is Required")]
        public int ElectricityAvailible { get; set; }

        [Display(Name = "Measure By Installer")]
        [Required(ErrorMessage = "Measure by Installer is Required")]
        public int MeasureByInstaller { get; set; }

        [Display(Name = "Leave Samples By Installer")]
        [Required(ErrorMessage = "Leave Samples By Installer is Required")]
        public int LeaveSamplesByInstaller { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "Finish Date")]
        public string FinishDate { get; set; }

        [Display(Name = "Hard Date")]
        public int HardDate { get; set; }

        [Display(Name = "Pre Make Gates")]
        [Required(ErrorMessage = "Pre Make Gates is Required")]
        public int PremakeGate { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription1 { get; set; }

        [Display(Name = "Gate Installation")]
        public string GateInstallationID1 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription2 { get; set; }

        [Display(Name = "Gate Installation")]
        public string GateInstallationID2 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription3 { get; set; }

        [Display(Name = "Gate Installation")]
        public string GateInstallationID3 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription4 { get; set; }

        [Display(Name = "Gate Installation")]
        public string GateInstallationID4 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription5 { get; set; }

        [Display(Name = "Gate Installation")]
        public string GateInstallationID5 { get; set; }
        
        [Display(Name = "Scope")]
        [Required(ErrorMessage = "Scope is Required")]
        public string Scope { get; set; }

        [Display(Name = "Budgeted Install Days")]
        [Required(ErrorMessage = "Budgeted Install Days is Required")]
        public decimal BudgetedInstallDays { get; set; }

        public List<DOC10_Responses> ListInstallationResponses { get; set; }

        public List<DOC05_GateInstallation> ListGateInstallation { get; set; }

        public List<SYS14_DigType> ListDigType { get; set; }








    }
}