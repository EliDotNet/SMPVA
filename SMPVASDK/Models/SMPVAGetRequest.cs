using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Models
{
    /// <summary>
    /// class to be used to structure any get request to smpva api
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVAGetRequest
    {
        private string method;
        public string Method
        {
            get { return method; }
            set { method = "?metod=" + value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = "&number=" + value; }
        }

        private string apiKey;
        public string ApiKey
        {
            get { return apiKey; }
            set { apiKey = "&apikey=" + value; }
        }

        private string serviceCode;
        public string ServiceCode
        {
            get { return serviceCode; }
            set { serviceCode = "&service=" + value; }
        }

        private string id;
        public string ID
        {
            get { return id; }
            set { id = "&id=" + value; }
        }

        private string serviceID;
        public string ServiceID
        {
            get { return serviceID; }
            set { serviceID = "&service_id=" + value; }
        }


        public override string ToString()
        {
            string getrequestLink = string.Concat(SMPVA.DefaultLink, Method, ServiceCode, ApiKey);

            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrWhiteSpace(ID) &&
              (string.IsNullOrEmpty(ServiceID) || string.IsNullOrWhiteSpace(ServiceID)))
            {
                getrequestLink = string.Concat(SMPVA.DefaultLink, Method, ServiceCode, ID, ApiKey);
            }
            else if (!string.IsNullOrEmpty(ServiceID) && !string.IsNullOrWhiteSpace(ServiceID) &&
                    (string.IsNullOrEmpty(ID) || string.IsNullOrWhiteSpace(ID)) &&
                    string.IsNullOrEmpty(Number) && string.IsNullOrWhiteSpace(Number))
            {
                getrequestLink = string.Concat(SMPVA.DefaultLink, Method, ServiceCode, ApiKey, ServiceID);
            }
            else if (!string.IsNullOrEmpty(ServiceID) && !string.IsNullOrWhiteSpace(ServiceID) &&
                     !string.IsNullOrEmpty(ID) && !string.IsNullOrWhiteSpace(ID) &&
                    string.IsNullOrEmpty(Number) && string.IsNullOrWhiteSpace(Number))
            {
                getrequestLink = string.Concat(SMPVA.DefaultLink, Method, ServiceCode, ID, ApiKey, ServiceID);
            }
            else if (!string.IsNullOrEmpty(Number) && !string.IsNullOrWhiteSpace(Number))
            {
                getrequestLink = string.Concat(SMPVA.DefaultLink, Method, ServiceCode, Number, ApiKey);
            }

            return getrequestLink;
        }
    }
}
