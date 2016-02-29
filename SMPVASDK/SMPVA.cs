using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMPVASDK.Errors;
using SMPVASDK.Models;
using SMPVASDK.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK
{
    /// <summary>
    /// Use this class to structure all or your smpva api requests.
    /// 
    /// for more info visit http://www.smspva.com/new_theme_api.html
    /// </summary>
    public class SMPVA
    {
        public const string DefaultLink = "http://smspva.com/priemnik.php";

        public string ApiKey { get; set; }

        private SMPVAServicesDictionary mResouceCodeDictionary = new SMPVAServicesDictionary();
        /// <summary>
        /// Service and service_id dictionary 
        /// key:display name
        /// value:SMPVAServiceCode
        /// </summary>
        public SMPVAServicesDictionary ResouceCodeDictionary { get { return mResouceCodeDictionary; } }

        private SMPVAErrorCode errorCodes = new SMPVAErrorCode();

        public SMPVA() { }
        public SMPVA(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// use this method when you are ready to get the varification code sent via sms
        /// </summary>
        /// <param name="request">the structured request for more info visit http://www.smspva.com/new_theme_api.html </param>
        /// <returns></returns>
        public SMPVAGetSMSResponse GetCodeResponse(SMPVAGetRequest request)
        {
            SetApiKeyIfNeeded(request);

            string webResponse = GetSMPVAResponse(request.ToString());

            return JsonConvert.DeserializeObject<SMPVAGetSMSResponse>(webResponse);
        }

        /// <summary>
        /// use this method when you are ready to retreive a number for a service
        /// once you have a number you can now continue with that number with a get sms request in GetCodeResponse
        /// </summary>
        /// <param name="request">the structured request for more info visit http://www.smspva.com/new_theme_api.html </param>
        /// <returns></returns>
        public SMPVAGetNumberResponse GetNumberResponse(SMPVAGetRequest request)
        {
            SetApiKeyIfNeeded(request);

            string webResponse = GetSMPVAResponse(request.ToString());

            return JsonConvert.DeserializeObject<SMPVAGetNumberResponse>(webResponse);
        }

        /// <summary>
        /// use this method when you are ready to retreive a counts reply for a service
        /// if activations is greater then 0 continue with a get number request in GetNumberResponse
        /// </summary>
        /// <param name="request">the structured request for more info visit http://www.smspva.com/new_theme_api.html </param>
        /// <returns></returns>
        public SMPVAGetCountResponse GetCountResponse(SMPVAGetRequest request)
        {
            SetApiKeyIfNeeded(request);

            string webResponse = GetSMPVAResponse(request.ToString());

            JObject json = JsonConvert.DeserializeObject<JObject>(webResponse);

            return new SMPVAGetCountResponse()
            {
                response = (string)json["response"],
                countsType = ((JProperty)json.Last).Name,
                activations = (long)json[((JProperty)json.Last).Name],
            }; ;
        }

        /// <summary>
        /// use this method when you are ready to retreive the api balance if less than 5 do not continue.
        /// if it is more then 5 continue with a get count request in GetCountResponse
        /// </summary>
        /// <param name="request">the structured request for more info visit http://www.smspva.com/new_theme_api.html </param>
        /// <returns></returns>
        public SMPVAGetBalanceResponse GetBalenceResponse(SMPVAGetRequest request)
        {
            SetApiKeyIfNeeded(request);

            string webResponse = GetSMPVAResponse(request.ToString());

            return JsonConvert.DeserializeObject<SMPVAGetBalanceResponse>(webResponse);
        }

        private void SetApiKeyIfNeeded(SMPVAGetRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ApiKey) || string.IsNullOrEmpty(request.ApiKey))
                request.ApiKey = ApiKey;
        }

        private string GetSMPVAResponse(string link)
        {
            var request = (HttpWebRequest)WebRequest.Create(link);
            request.Method = "GET";
            var webResponse = (HttpWebResponse)request.GetResponse();

            string response = null;
            using (var stream = new StreamReader(webResponse.GetResponseStream()))
            {
                response = stream.ReadToEnd();
            }

            if (errorCodes.ErrorCodes.ContainsKey(response))
                throw new SMPVAException(errorCodes.ErrorCodes[response]);

            return response;
        }
    }
}
