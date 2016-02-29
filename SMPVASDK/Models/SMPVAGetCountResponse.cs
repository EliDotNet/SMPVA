using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Models
{
    /// <summary>
    /// data resulting from a get_count request
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVAGetCountResponse
    {
        public string response { get; set; }
        public string countsType { get; set; }
        public long activations { get; set; }

        public override string ToString()
        {
            return string.Concat("Server Response code: ", response == "1" ? "OK" : "Bad Request", " Counts type: ", countsType, ", Counts: ", activations);
        }
    }
}
