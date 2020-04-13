using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class ProjectChecklistHeader
    {
        public string ProjectHeaderId { get; set; }
        public string Atlas_Job_Number { get; set; }
        public string Atlas_Company_Name { get; set; }
        public string Contractor_Name { get; set; }
        public string Contractor_Address { get; set; }
        public string Contractor_City { get; set; }
        public string Contractor_State { get; set; }
        public string Contractor_Zip { get; set; }
        public string Contractor_Phone { get; set; }
        public string JobName { get; set; }
        public string JobAddress { get; set; }
        public string JobCity { get; set; }
        public string JobState { get; set; }
        public string JobZip { get; set; }
        public string JobContact { get; set; }
        public string JobPhone { get; set; }
        public string EstimatorsName { get; set; }
    }
}