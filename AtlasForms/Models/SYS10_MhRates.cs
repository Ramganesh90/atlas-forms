using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class SYS10_MhRates
    {
        public string MhRateId { get; set; }
        public string MhRateName { get; set; }
        public string CompanyId { get; set; }
        public int? WageTypeId { get; set; }
        public string State { get; set; }
        public int? TagColorCode { get; set; }
    }
}