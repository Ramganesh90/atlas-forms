using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AtlasForms.DataAccess.Entity
{
    public class AppUtil
    {
        public static string formatPhoneNumber(string phoneNum, string phoneFormat="")
        {
            if (string.IsNullOrWhiteSpace(phoneNum))
            {
                return "-";
            }

            if (phoneFormat == "")
            {
                // If phone format is empty, code will use default format (###) ###-####
                phoneFormat = "(###) ###-####";
            }

            // First, remove everything except of numbers
            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");

            // Second, format numbers to phone string
            if (phoneNum.Length > 0)
            {
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            } 

            return phoneNum;
        }

        public static string formatText(object strVal)
        {
            if (strVal == null) return "-";
            string str = Convert.ToString(strVal);
            if (string.IsNullOrWhiteSpace(str))
            {
                return "-";
            }
            else
                return str;
        }
    }
}