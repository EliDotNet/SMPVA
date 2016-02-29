using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Errors
{
    public class SMPVAException : Exception
    {
        string smpvaErrorMessage = "smpva server error";

        public SMPVAException(string errorMessage)
        {
            smpvaErrorMessage = errorMessage;
        }

        public override string Message
        {
            get
            {
                return smpvaErrorMessage;
            }
        }
    }
}
