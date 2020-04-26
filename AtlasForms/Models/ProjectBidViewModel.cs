using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class ProjectBidViewModel
    {
        [Required(ErrorMessage ="Project ID Required")]
        [Display(Name = "Project Number")]
        public string ProjectHeaderId { get; set; }
        [Display(Name = "Bid Item Number")]
        [Required(ErrorMessage = "Bid Item ID Required")]
        public string BidItemId { get; set; }
        public string HardCardId { get; set; }

        public List<string> BidItemsList { get; set; }
    }
}