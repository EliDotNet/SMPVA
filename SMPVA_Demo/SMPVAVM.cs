using SMPVASDK;
using SMPVASDK.Models;
using SMPVASDK.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SMPVA_Demo
{
    public enum ActivationStates
    {
        GetBalance,
        RequestActivations,
        GettingThePhoneNumber,
        RequestingSMS,
    }
    public class SMPVAVM : INotifyPropertyChanged
    {
        public ICommand OnCommandFromView { get; set; }

        public ObservableCollection<SMPVAService> AvailableServices { get; set; }
        //SelectedService
        private SMPVAService selectedService;
        public SMPVAService SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                ProgressUpdates = SelectedService.ServiceDisplayName;
                CheckHasNumberAndServiceSelected();
                RaisePropertyChanged("SelectedService");
            }
        }
        public string SelectedServiceCode { get { return mSmpva.ResouceCodeDictionary.SMPVAServices[SelectedService.ServiceDisplayName].serviceCode; } }
        public string SelectedServiceID { get { return mSmpva.ResouceCodeDictionary.SMPVAServices[SelectedService.ServiceDisplayName].serviceID; } }

        //ApiKey
        private string apikey;
        public string ApiKey
        {
            get { return apikey; }
            set { apikey = value; RaisePropertyChanged("ApiKey"); }
        }

        //PhoneNumberReply
        private string phoneNumberReply;
        public string PhoneNumberReply
        {
            get { return phoneNumberReply; }
            set
            {
                phoneNumberReply = value;
                CheckHasNumberAndServiceSelected();
                RaisePropertyChanged("PhoneNumberReply");
            }
        }

        //ActivationCode
        private string activationCode;
        public string ActivationCode
        {
            get { return activationCode; }
            set { activationCode = value; RaisePropertyChanged("ActivationCode"); }
        }

        //ProgressUpdates
        private string progressUpdates;
        public string ProgressUpdates
        {
            get { return progressUpdates; }
            set { progressUpdates = value; RaisePropertyChanged("ProgressUpdates"); }
        }

        //ButtonText
        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; RaisePropertyChanged("ButtonText"); }
        }

        //BtnEnabled
        private bool btnEnabled;
        public bool BtnEnabled
        {
            get { return btnEnabled; }
            set { btnEnabled = value; RaisePropertyChanged("BtnEnabled"); }
        }

        //SelectServiceEnabled
        private bool selectServiceEnabled;
        public bool SelectServiceEnabled
        {
            get { return selectServiceEnabled; }
            set { selectServiceEnabled = value; RaisePropertyChanged("SelectServiceEnabled"); }
        }

        //VisibleBan
        private Visibility visibleGotNumber;
        public Visibility VisibleGotNumber
        {
            get { return visibleGotNumber; }
            set { visibleGotNumber = value; RaisePropertyChanged("VisibleGotNumber"); }
        }

        //VisibleHasPhoneText
        private Visibility visibleHasPhoneText;
        public Visibility VisibleHasPhoneText
        {
            get { return visibleHasPhoneText; }
            set { visibleHasPhoneText = value; RaisePropertyChanged("VisibleHasPhoneText"); }
        }


        private ActivationStates mActivationState = ActivationStates.GetBalance;
        private SMPVA mSmpva;

        private SMPVAGetNumberResponse mNumberesponse;
        private SMPVAGetNumberResponse mProverkaresponse;
        private SMPVAGetCountResponse mCountResponse;
        private SMPVAGetBalanceResponse mBalenceResponse;
        private SMPVAGetSMSResponse mCodeResponse;

        public SMPVAVM()
        {
            OnCommandFromView = new RelayCommand(OnCommandFromView_Raised);

            ApiKey = "";
            mSmpva = new SMPVA();

            //uncomment and use your api key here to initialize smpva service with a api key
            //ApiKey = "API_KEY_HERE";
            //mPhoneRegistrator = new SMPVA(ApiKey);

            AvailableServices = new ObservableCollection<SMPVAService>();
            foreach (var i in mSmpva.ResouceCodeDictionary.SMPVAServices)
            {
                AvailableServices.Add(new SMPVAService() { ServiceDisplayName = i.Key });
            }

            Restart();

        }

        private void Restart()
        {
            ButtonText = "OK";
            ProgressUpdates = "Select Service";
            PhoneNumberReply = "";
            ActivationCode = "";
            BtnEnabled = true;
            SelectServiceEnabled = true;
            VisibleGotNumber = Visibility.Collapsed;
            mActivationState = ActivationStates.GetBalance;

            mNumberesponse = null;
            mCountResponse = null;
            mBalenceResponse = null;
            mCodeResponse = null;
        }


        private void OnCommandFromView_Raised(object param)
        {
            Task.Factory.StartNew(() =>
            {
                BtnEnabled = false;
                try
                {
                    if (SelectedService == null) throw new Exception("Select A Service.");

                    switch ((string)param)
                    {
                        case "RESTART":
                            Restart();
                            break;

                        case "BAN":
                            ThrowNoNumFoundExceptionIfNeeded();
                            ProgressUpdates = "Getting Ban";
                            mNumberesponse = mSmpva.GetNumberResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.Ban, ServiceCode = SelectedServiceCode, ID = mNumberesponse.id });
                            ProgressUpdates = mNumberesponse.ToString();
                            if (mNumberesponse.response == "1")
                            {
                                ButtonText = "Request Code or Check Blocked";
                                PhoneNumberReply = mNumberesponse.number;
                                mActivationState = ActivationStates.RequestingSMS;
                            }
                            break;

                        case "PROVERKA":
                            ProgressUpdates = "Checking if number Exists...";
                            mProverkaresponse = mSmpva.GetNumberResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.Proverka, ServiceCode = SelectedServiceCode, Number = PhoneNumberReply });
                            ProgressUpdates = mProverkaresponse.ToString();
                            if (mProverkaresponse.response == "ok")
                            {
                                mNumberesponse = mProverkaresponse;
                                ButtonText = "Request Code or Check Blocked";
                                PhoneNumberReply = mNumberesponse.number;
                                SelectServiceEnabled = false;
                                mActivationState = ActivationStates.RequestingSMS;
                                VisibleGotNumber = Visibility.Visible;
                            }
                            break;

                        case "DENY":
                            ThrowNoNumFoundExceptionIfNeeded();
                            ProgressUpdates = "Denying Number";
                            mNumberesponse = mSmpva.GetNumberResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.Denial, ServiceCode = SelectedServiceCode, ID = mNumberesponse.id });
                            ProgressUpdates = mNumberesponse.ToString();
                            if (mNumberesponse.response == "1")
                            {
                                Restart();
                            }
                            break;

                        case "CONTINUE":
                            switch (mActivationState)
                            {
                                case ActivationStates.GetBalance:
                                    VisibleGotNumber = Visibility.Collapsed;
                                    ProgressUpdates = "Getting your ballence";
                                    mBalenceResponse = mSmpva.GetBalenceResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.GetBalence, ServiceCode = SelectedServiceCode });
                                    ProgressUpdates = mBalenceResponse.ToString();
                                    if (mBalenceResponse.HassSuffecitentFunds && mBalenceResponse.response == "1")
                                    {
                                        ProgressUpdates += Environment.NewLine + "Getting Activations...";
                                        ButtonText = "Get Activations";
                                        SelectServiceEnabled = false;
                                        mActivationState = ActivationStates.RequestActivations;
                                        OnCommandFromView_Raised("CONTINUE");
                                    }
                                    break;

                                case ActivationStates.RequestActivations:
                                    ProgressUpdates = "Activateing Your Request";
                                    mCountResponse = mSmpva.GetCountResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.GetCount, ServiceCode = SelectedServiceCode, ServiceID = SelectedServiceID });
                                    ProgressUpdates = mCountResponse.ToString();
                                    if (mCountResponse.response == "1" && mCountResponse.activations > 0)
                                    {
                                        ProgressUpdates += Environment.NewLine + "Waiting 4 Seconds Then Getting PhoneNumber...";
                                        ButtonText = "Get Phone Number";
                                        mActivationState = ActivationStates.GettingThePhoneNumber;
                                        Thread.Sleep(4500);
                                        OnCommandFromView_Raised("CONTINUE");
                                    }
                                    break;

                                case ActivationStates.GettingThePhoneNumber:
                                    ProgressUpdates = "Getting Phone Number";
                                    BtnEnabled = false;
                                    mNumberesponse = mSmpva.GetNumberResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.GetNumber, ServiceCode = SelectedServiceCode, ID = "1" });
                                    ProgressUpdates = mNumberesponse.ToString();
                                    if (mNumberesponse.response == "1")
                                    {
                                        ButtonText = "Get Code";
                                        PhoneNumberReply = mNumberesponse.number;
                                        mActivationState = ActivationStates.RequestingSMS;
                                        VisibleGotNumber = Visibility.Visible;
                                    }
                                    break;

                                case ActivationStates.RequestingSMS:
                                    ProgressUpdates = "Getting Code";
                                    mCodeResponse = mSmpva.GetCodeResponse(new SMPVAGetRequest() { ApiKey = this.ApiKey, Method = SMPVARequestMethods.GetSMS, ServiceCode = SelectedServiceCode, ID = mNumberesponse.id });
                                    ProgressUpdates = mCodeResponse.ToString();
                                    if (mCodeResponse.response == "1" || mCodeResponse.response == "2")
                                    {
                                        ButtonText = "Get Code";
                                        PhoneNumberReply = mCodeResponse.number;
                                        ActivationCode = mCodeResponse.sms;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ProgressUpdates = "Something went wrong while processing youe request. Error Message:" + ex.Message;
                }
                BtnEnabled = true;
            });
        }

        private void CheckHasNumberAndServiceSelected()
        {
            if (!string.IsNullOrEmpty(phoneNumberReply) &&
                    !string.IsNullOrWhiteSpace(phoneNumberReply) &&
                    phoneNumberReply.Length == 10 &&
                    SelectedService != null)
            {
                VisibleHasPhoneText = Visibility.Visible;
            }
            else
            {
                VisibleHasPhoneText = Visibility.Collapsed;
            }
        }

        private void ThrowNoNumFoundExceptionIfNeeded()
        {
            if (mNumberesponse == null)
                throw new Exception("No number found... restart the process.");
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
