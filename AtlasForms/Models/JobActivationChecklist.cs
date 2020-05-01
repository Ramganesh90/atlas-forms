using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class JobActivationChecklist
    {
        public int PRJID { get; set; }
        public ProjectInformation projectInformation { get; set; }
        public ContractPaperWork contractPaperWork { get; set; }
        public BondingInsurance bondingInsurance { get; set; }
        public SafetyRequirements safetyRequirements { get; set; }
        public OtherImportantFactors otherImportantFactors { get; set; }


        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [Display(Name = "Date Completed")]
        [Required(ErrorMessage = "Date Completed is Required")]
        public string DateCompleted { get; set; }
        [Display(Name = "Date Reviewed")]
        public string DateReviewed { get; set; }

        public string recordExists { get; set; }

        public List<SYS10_MhRates> ListLabourTypes { get; set; }

        public List<DOC08_CustomerType> ListCustomersTypes { get; set; }
        public List<DOC09_JobTypes> ListJobTypes { get; set; }
        public List<Sys_UsersData> ListUsers { get; set; }
        public List<DOC10_Responses> ListResponses { get; set; }

    }
    public class ProjectInformation
    {
        [Display(Name = "Atlas Job Number")]
        [Required(ErrorMessage = "Atlas Job Number is Required")]
        public string JobNumber { get; set; }
        [Display(Name = "Atlas Company Name")]
        public string CompanyName { get; set; }
        public Profile CustomerProfile { get; set; }
        public Profile ProjectProfile { get; set; }
        [Display(Name = "Customer Type")]
        [Required(ErrorMessage = "Customer Type is Required")]
        public string CustomerType { get; set; }
        [Display(Name = "Job Type")]
        [Required(ErrorMessage = "Job Type is Required")]
        public string Jobtype { get; set; }
        [Display(Name = "Customer Bid/Job Reference #")]        
        public string CustomerBidReference { get; set; }
        [Display(Name = "Scope of Work to be performed")]
        [Required(ErrorMessage = "Scope of Work is Required")]
        public string ScopeWorkToBePerformed { get; set; }
        [Display(Name = "Type of Labor")]
        public string TypeOfLabour { get; set; }

        [Display(Name = "Estimator Name")]
        public string Estimator { get; set; }

    }
    public class ContractPaperWork
    {
        [Display(Name = "Copy of Contract or PO")]
        [Required(ErrorMessage = "Contractor Name is Required")]
        public string CopyOfContractorPO { get; set; }
        [Display(Name = "Comments")]
        public string CopyOfContractorPOComments { get; set; }
        [Display(Name = "Broken scope into appropriate phases")]
        [Required(ErrorMessage = "Broken Scope Phase is Required")]
        public string BrokenScopephases { get; set; }
        [Display(Name = "Comments")]
        public string BrokenScopephasesComments { get; set; }
        [Display(Name = "Bid Roll-up (For each BI being activated)")]
        [Required(ErrorMessage = "Bid Roll Up is Required")]
        public string BidRollUp { get; set; }
        [Display(Name = "Comments")]
        public string BidRollUpComments { get; set; }
        [Display(Name = "Pack CFS for each BI rollup")]
        [Required(ErrorMessage = "Pack CFS is Required")]
        public string PackCFSPIRollUp { get; set; }
        [Display(Name = "Comments")]
        public string PackCFSPIRollUpcomments { get; set; }
        [Display(Name = "Applicable quote(s) for special material")]
        [Required(ErrorMessage = "Applicable Quotes is Required")]
        public string ApplicableQuote { get; set; }
        [Display(Name = "Comments")]
        public string ApplicableQuoteComments { get; set; }
        [Display(Name = "Drawings/field conditions or grading reports")]
        [Required(ErrorMessage = "Drawing Conditions is Required")]
        public string DrawingConditions { get; set; }
        [Display(Name = "Comments")]
        public string DrawingConditionsComments { get; set; }
        [Display(Name = "Site Photos")]
        [Required(ErrorMessage = "Site Photos is Required")]
        public string SitePhotos { get; set; }
        [Display(Name = "Comments")]
        public string SitePhotosComments { get; set; }
        [Display(Name = "Hard Card completely filled out")]
        [Required(ErrorMessage = "Hard Card Details is Required")]
        public string HardCard { get; set; }
        [Display(Name = "Comments")]
        public string HardCardComments { get; set; }
        [Display(Name = "Pay Envelope (residential only)")]
        [Required(ErrorMessage = "Pay Envelope is Required")]
        public string PayEnvelope { get; set; }
        [Display(Name = "Comments")]
        public string PayEnvelopeComments { get; set; }


    }
    public class BondingInsurance
    {
        [Display(Name = "Received bond (if required) and necessary insurance certification")]
        [Required(ErrorMessage = "Bond Insurance is Required")]
        public string InsuranceCertification { get; set; }
        [Display(Name = "Comments")]
        public string InsuranceCertificationComments { get; set; }

    }
    public class SafetyRequirements
    {
        [Display(Name = "Is there a safety officer on site? If so, provide Contact Information")]
        [Required(ErrorMessage = "Safety Office Contact is Required")]
        public string SafetyOfficer { get; set; }
        [Display(Name = "Comments")]
        public string SafetyOfficerComments { get; set; }
        [Display(Name = "Any safety meetings/orientation or badging needed before the job starts?")]
        [Required(ErrorMessage = "Safety Meeting details is Required")]
        public string SafetyMeeting { get; set; }
        [Display(Name = "Comments")]
        public string SafetyMeetingComments { get; set; }
        [Display(Name = "Any daily safety meetings, truck/tool inspections required?")]
        [Required(ErrorMessage = "Daily safety Meeting details is Required")]
        public string DailySafetyMeeting { get; set; }
        [Display(Name = "Comments")]
        public string DailySafetyMeetingComments { get; set; }
        [Display(Name = "Any specific PPE needed (e.g. fall protection,flotation, fire clothing, etc.)")]
        [Required(ErrorMessage = "PPE is Required")]
        public string PPENeeded { get; set; }
        [Display(Name = "Comments")]
        public string PPENeededComments { get; set; }
        [Display(Name = "Is fall protection is required? Please provide details of the situation ?")]
        [Required(ErrorMessage = "Fall Protection is Required")]
        public string FallProtection { get; set; }
        [Display(Name = "Comments")]
        public string FallProtectionComments { get; set; }
        [Display(Name = "Any equipment certifications required (e.g. Bobcats, Forklifts, High lift, etc)?")]
        [Required(ErrorMessage = "Equipment Certification is Required")]
        public string EquipmentCertification { get; set; }
        [Display(Name = "Comments")]
        public string EquipmentCertificationComments { get; set; }
        [Display(Name = "Other hazards (water, lane closure, dust/respirator, HEPA, vacuum, heavy lifting)")]
        [Required(ErrorMessage = "Other hazards is Required")]
        public string OtherHazards { get; set; }
        [Display(Name = "Comments")]
        public string OtherHazardsComments { get; set; }

    }
    public class OtherImportantFactors
    {
        [Display(Name = "Please fill in any other pertinant information")]
        public string OtherPertinentInformation { get; set; }

    }

    public class Profile
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
    }

}