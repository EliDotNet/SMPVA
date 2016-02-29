using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Models
{
    /// <summary>
    /// data resulting from a get_balance request
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVAGetBalanceResponse
    {
        public string response { get; set; }
        public double balance { get; set; }

        public bool HassSuffecitentFunds { get { return balance > 5; } }

        public override string ToString()
        {
            return string.Concat("Server Response code: ", response == "1" ? "OK" : "Bad Request", ", Balance: ", balance);
        }
    }
}
