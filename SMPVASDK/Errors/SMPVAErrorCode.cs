using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Errors
{
    public class SMPVAErrorCode
    {
        public Dictionary<string, string> ErrorCodes = new Dictionary<string, string>();

        public SMPVAErrorCode()
        {
            ErrorCodes.Add("", "Server Did not reply with a meaningfull response");
            ErrorCodes.Add("API KEY не получен!", "Service is not defined!");
            ErrorCodes.Add("Сервис не определён", "Invalid API KEY has been entered");
            ErrorCodes.Add("Недостаточно средств!", "Insufficient funds");
            ErrorCodes.Add("Превышено количество попыток!", "Set a longer interval between calls to API server");
            ErrorCodes.Add("Произошла неизвестная ошибка.", "Try to repeat your request later.");
            ErrorCodes.Add("Неверный запрос.", "Check the request syntax and the list of parameters used (can be found on the page with method description).");
            ErrorCodes.Add("Произошла внутренняя ошибка сервера.", "Try to repeat your request later.");
            ErrorCodes.Add("{\"response\":\"5\",\"number\":null,\"id\":0,\"text\":null,\"extra\":null,\"sms\":null}", "Set a longer interval between calls to API server");
        }
    }
}
