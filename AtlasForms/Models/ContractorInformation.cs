using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class ContractorInformation
    {
        [Display(Name = "Contractor Name",Description ="ContName")]
        public string ContractorName { get; set; }

        [Display(Name = "Primary Contact Name",Description ="ContContact")]
        public string PrimaryContact { get; set; }

        [Display(Name = "Phone", Description ="ContPhone")]
        public string PrimaryPhone { get; set; }

        [Display(Name = "FenceType")]
        [Required(ErrorMessage = "Fence Type is Required")]
        public string FenceType { get; set; }

        [Display(Name = "Special Notes")]
        public string SplNotes { get; set; }

        [Display(Name = "Directions")]
        public string Directions { get; set; }

        public List<SYS07_FenceTypes> ListFenceTypes { get; set; }

    }
}