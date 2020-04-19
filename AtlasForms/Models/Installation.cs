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
        public int CBYD { get; set; }

        [Display(Name = "CBYD Date")]
        public string CBYDDate { get; set; }

        [Display(Name = "CBYD Number")]
        public string CBYDNumber { get; set; }

        [Display(Name = "Equipment Required")]
        public string EquipmentRequired { get; set; }

        [Display(Name = "DigTypeID")]
        public int DigTypeID { get; set; }

        [Display(Name = "Water Available")]
        public int WaterAvailible { get; set; }

        [Display(Name = "Electricity Available")]
        public int ElectricityAvailible { get; set; }

        [Display(Name = "Measure By Installer")]
        public int MeasureByInstaller { get; set; }

        [Display(Name = "Leave Samples By Installer")]
        public int LeaveSamplesByInstaller { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "Finish Date")]
        public string FinishDate { get; set; }

        [Display(Name = "Hard Date")]
        public int HardDate { get; set; }

        [Display(Name = "Pre Make Gates")]
        public int PremakeGate { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription1 { get; set; }

        [Display(Name = "Gate Installation")]
        public int GateInstallationID1 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription2 { get; set; }

        [Display(Name = "Gate Installation")]
        public int GateInstallationID2 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription3 { get; set; }

        [Display(Name = "Gate Installation")]
        public int GateInstallationID3 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription4 { get; set; }

        [Display(Name = "Gate Installation")]
        public int GateInstallationID4 { get; set; }

        [Display(Name = "Gate Description")]
        public string GateDescription5 { get; set; }

        [Display(Name = "Gate Installation")]
        public int GateInstallationID5 { get; set; }
        
        [Display(Name = "Scope")]
        public string Scope { get; set; }

        [Display(Name = "Budgeted Install Days")]
        public decimal BudgetedInstallDays { get; set; }

        public List<DOC10_Responses> ListInstallationResponses { get; set; }

        public List<DOC05_GateInstallation> ListGateInstallation { get; set; }

        public List<SYS14_DigType> ListDigType { get; set; }








    }
}