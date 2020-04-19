using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class SYS07_FenceTypes
    {
        public int FenceTypeId { get; set; }
        public string FenceTypeName { get; set; }
        public string CompanyId { get; set; }
        public string FenceFamily { get; set; }
        public int FutureFenceTypeId { get; set; }
        public int WageTypeId { get; set; }
    }
}