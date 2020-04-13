using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasForms.Models
{
    public class PRL03_Users
    {
        public int UserId { get; set; }
        public string EmployeeId { get; set; }
        public bool Admin { get; set; }
        public bool Sales { get; set; }
        public string RoleID { get; set; }
        public string ContractPhoneNumber { get; set; }
        public string GlobalFontFace { get; set; }
        public string ColorScheme { get; set; }
        public string GlobalBackgroundColor { get; set; }
        public string HeaderColor { get; set; }
        public string HeadingTextColor { get; set; }
        public string FormColor { get; set; }
        public string AccentColor { get; set; }
        public string EvenNumberedDataRow { get; set; }
        public string OddNumberedDataRow { get; set; }
        public string LinkColor { get; set; }
        public string VisitedLinkColor { get; set; }
        public string DisplayProjectYear { get; set; }
        public bool EnableDelete { get; set; }
        public string EmailAddress { get; set; }
        public string WinLogon { get; set; }
        public string WinPassword { get; set; }
        public string MasPassword { get; set; }
        public string TelPassword { get; set; }
        public string ComputerName { get; set; }
        public bool? Active { get; set; }
    }
}