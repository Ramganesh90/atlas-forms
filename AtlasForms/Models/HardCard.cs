using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
  public class HardCard
    {
   
        public int HardCardId { get; set; }

        public int ProjectHeaderId { get; set; }

        public int BidItemHeaderid { get; set; }

        public JobInformation JobInformationDetails { get; set; }

        public ContractorInformation ContractInformationDetails { get; set; }

        public Installation InstallationDetails { get; set; }


        public BuildChecklist BuildChecklistDetails { get; set; }


    }
}