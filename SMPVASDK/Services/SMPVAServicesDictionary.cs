using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPVASDK.Services
{
    public class SMPVAServicesDictionary
    {
        public Dictionary<string, SMPVAServiceCode> SMPVAServices { get; private set; }

        public SMPVAServicesDictionary()
        {
            SMPVAServices = new Dictionary<string, SMPVAServiceCode>();
            SMPVAServices.Add("4GAME", new SMPVAServiceCode { serviceCode = "opt0", serviceID = "4game" });
            SMPVAServices.Add("GMail", new SMPVAServiceCode { serviceCode = "opt1", serviceID = "gmail" });
            SMPVAServices.Add("Facebook", new SMPVAServiceCode { serviceCode = "opt2", serviceID = "fb" });
            SMPVAServices.Add("Spaces.ru", new SMPVAServiceCode { serviceCode = "opt3", serviceID = "spaces" });
            SMPVAServices.Add("Вконтакте", new SMPVAServiceCode { serviceCode = "opt4", serviceID = "vk" });
            SMPVAServices.Add("Одноклассники", new SMPVAServiceCode { serviceCode = "opt5", serviceID = "ok" });
            SMPVAServices.Add("Mamba", new SMPVAServiceCode { serviceCode = "opt7", serviceID = "mamba" });
            SMPVAServices.Add("LinkedIn", new SMPVAServiceCode { serviceCode = "opt8", serviceID = "avito" });
            SMPVAServices.Add("Viber", new SMPVAServiceCode { serviceCode = "opt11", serviceID = "viber" });
            SMPVAServices.Add("Фотострана", new SMPVAServiceCode { serviceCode = "opt13", serviceID = "fotostrana" });
            SMPVAServices.Add("MS, Live, Bing, Hotmail", new SMPVAServiceCode { serviceCode = "opt15", serviceID = "ms" });
            SMPVAServices.Add("Instagram", new SMPVAServiceCode { serviceCode = "opt16", serviceID = "instagram" });
            SMPVAServices.Add("Qiwi с отвязкой", new SMPVAServiceCode { serviceCode = "opt18", serviceID = "qiwi" });
            SMPVAServices.Add("Другое", new SMPVAServiceCode { serviceCode = "opt19", serviceID = "others" });
            SMPVAServices.Add("WhatsAPP", new SMPVAServiceCode { serviceCode = "opt20", serviceID = "whatsapp" });
            SMPVAServices.Add("Webtransfer", new SMPVAServiceCode { serviceCode = "opt21", serviceID = "webtransfer" });
            SMPVAServices.Add("SEOsprint.net", new SMPVAServiceCode { serviceCode = "opt22", serviceID = "seosprint" });
            SMPVAServices.Add("Яндекс", new SMPVAServiceCode { serviceCode = "opt23", serviceID = "ya" });
            SMPVAServices.Add("WebMoney", new SMPVAServiceCode { serviceCode = "opt24", serviceID = "webmoney" });
            SMPVAServices.Add("NaSimke.ru", new SMPVAServiceCode { serviceCode = "opt25", serviceID = "nasimke" });
            SMPVAServices.Add("COM.NU", new SMPVAServiceCode { serviceCode = "opt26", serviceID = "com" });
            SMPVAServices.Add("Dodopizza.ru", new SMPVAServiceCode { serviceCode = "opt27", serviceID = "dodopizza" });
            SMPVAServices.Add("Tabor.ru", new SMPVAServiceCode { serviceCode = "opt28", serviceID = "tabor" });
            SMPVAServices.Add("Telegram", new SMPVAServiceCode { serviceCode = "opt29", serviceID = "telegram" });
            SMPVAServices.Add("Простоквашино", new SMPVAServiceCode { serviceCode = "opt30", serviceID = "prostock" });
            SMPVAServices.Add("Друг Вокруг", new SMPVAServiceCode { serviceCode = "opt31", serviceID = "drugvokrug" });
            SMPVAServices.Add("Drom.RU", new SMPVAServiceCode { serviceCode = "opt32", serviceID = "drom" });
            SMPVAServices.Add("Mail.RU", new SMPVAServiceCode { serviceCode = "opt33", serviceID = "mail" });
            //PRServices.Add("Ценобой", new SMPVAServiceCode { serviceCode = "opt34", serviceID = "" });
            //PRServices.Add("GetTaxi", new SMPVAServiceCode { serviceCode = "opt35", serviceID = "" });
            //PRServices.Add("VK Serfing", new SMPVAServiceCode { serviceCode = "opt37", serviceID = "" });
            //PRServices.Add("Auto.RU", new SMPVAServiceCode { serviceCode = "opt38", serviceID = "" });
            //PRServices.Add("like4u", new SMPVAServiceCode { serviceCode = "opt39", serviceID = "" });
            //PRServices.Add("VOXOX.COM", new SMPVAServiceCode { serviceCode = "opt40", serviceID = "" });
            SMPVAServices.Add("Twitter", new SMPVAServiceCode { serviceCode = "opt41", serviceID = "twitter" });
            //PRServices.Add("Авито", new SMPVAServiceCode { serviceCode = "opt59", serviceID = "" });
            //PRServices.Add("MasterCard", new SMPVAServiceCode { serviceCode = "opt71", serviceID = "" });
            //PRServices.Add("PremiaRuneta", new SMPVAServiceCode { serviceCode = "opt72", serviceID = "" });

        }
    }
}
