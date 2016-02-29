using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Models
{
    /// <summary>
    /// data resulting from a get_number request
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVAGetNumberResponse
    {
        public string response { get; set; }
        public string number { get; set; }
        public string id { get; set; }

        public string error_msg { get; set; }
        public string not_number { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(error_msg) && string.IsNullOrWhiteSpace(error_msg) &&
               string.IsNullOrEmpty(not_number) && string.IsNullOrWhiteSpace(not_number))
                return string.Concat("Server Response code: ", response == "1" || response == "ok" ? "OK" : "Bad Request", " Number: ", number, ", ID: ", id);
            else
                return string.Concat("Server Response: ", response, " Message: ", string.IsNullOrEmpty(not_number) && string.IsNullOrWhiteSpace(not_number) ? error_msg : not_number);
        }
    }
}
