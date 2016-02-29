using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Models
{
    /// <summary>
    /// data resulting from a get_sms request
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVAGetSMSResponse
    {
        public string response { get; set; }
        public string number { get; set; }
        public string sms { get; set; }

        public override string ToString()
        {
            return string.Concat("Server Response code: ", response == "1" ? "OK" : response == "2" ? "No Code Available" : "Bad Request", " Number: ", number, ", SMS: ", sms);
        }
    }
}
