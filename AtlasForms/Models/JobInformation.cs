using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
  public class JobInformation
    {
        [Display(Name = "Estimator")]
        public string EstimatorsName { get; set; }

        [Display(Name = "Job Number")]
        public string Atlas_Job_Number { get; set; }

        [Display(Name = "Est.Phone #")]
        public string ContractPhoneNumber { get; set; }

        [Display(Name = "BI  Number")]
        public int BidItemHeaderId { get; set; }

        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        [Display(Name = "Job Address")]
        public string JobAddress { get; set; }

        [Display(Name = "Job City")]
        public string JobCity { get; set; }

        [Display(Name = "Job State")]
        public string JobState { get; set; }

        [Display(Name = "Job Zip")]
        public string JobZip { get; set; }

        [Display(Name = "Job Contact")]
        public string JobContact { get; set; }

        [Display(Name = "Contact Phone")]
        public string JobPhone { get; set; }


        [Display(Name = "Call In Route")]
        [Required(ErrorMessage = "Call In Route is Required")]
        public string CallInRoute { get; set; }

        //TODO
        [Display(Name = "Contact Cell")]
        public string JobCell { get; set; }

        public List<DOC10_Responses> ListJobInfoResponse { get; set; }



    }
}