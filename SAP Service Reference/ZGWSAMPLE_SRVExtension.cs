using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.IW.SSO;
using SAP.IW.SSO.Utility;
using System.Net;
using System.Configuration;
using AdnWebAPI;
using AdnWebAPI.BusinessEntity;
using SAP.IW.GWPAM.Common.Configuration;
using SAP.Data.Service;


namespace AdnWebAPI.ZGWSAMPLE_SRV
{
    /// <summary>
    /// This is the extension class to the generated proxy class which contains eventhandler for SSO and an overloaded constructor accepting the SAP Client
    /// for Gateway services
    /// </summary>
    partial class ZGWSAMPLE_SRV
    {
        /// <summary>
        /// This Property must be set in order to connect to a service that doesn't use the default sap client
        /// </summary>
        public string SapClient { get; set; }


		/// <summary>
        /// Gets the atom delta token along with the tombstones. Tombstones contain the detail about the deleted item.
        /// </summary>
        public AtomDeltaTokens AtomDeltaToken { get; private set; }

        /// <summary>
        ///     Constructor of Entity Container that also receives sap client
        /// </summary>
        /// <param name="serviceRoot">The service root uri</param>
        /// <param name="sapClient">The sap client of the service</param>
        public ZGWSAMPLE_SRV(Uri serviceRoot, string sapClient)
            : this(serviceRoot)
        {
            this.SapClient = sapClient;
        }

        /// <summary>
        /// This method will add the event handler for the single sign on
        /// </summary>
        partial void OnContextCreated()
        {
            this.SendingRequest += new EventHandler<System.Data.Services.Client.SendingRequestEventArgs>(ZGWSAMPLE_SRV_SendingRequest);
        }

        /// <summary>
        ///     The event handler of this entity container which occurs on each
        ///     Request sent to the serivce. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ZGWSAMPLE_SRV_SendingRequest(object sender, System.Data.Services.Client.SendingRequestEventArgs e)
        {   
            HttpWebRequest webRequest = e.Request as HttpWebRequest;

            //ADN Modif
            ServiceDetails serviceDetail = null; // ConfigurationReaderHandler.Instance.GetServiceDetails("ZGWSAMPLE_SRV");
            
            if (serviceDetail == null)
            {
                serviceDetail = new ServiceDetails { Url = @"https://sapes1.sapdevcenter.com:443/sap/opu/odata/sap/ZGWSAMPLE_SRV/", Client = "", SSO = "BASIC" };
            }
			string language = System.Globalization.CultureInfo.CurrentUICulture.Name;
            webRequest.Headers["Accept-Language"] = language;
            BusinessConnectivityHelper.HandleSAPConnectivity(serviceDetail,ref webRequest);

			//The Below section helps you handle delta token. If you wish to use the details then pass the AtomDeltaToken back to your application.
            //To enable this section please set the value of "EnableDeltaToken" for the corresponding service to "Enabled".
            if (serviceDetail.HandleDeltaTokenDecision)
				{
					DataService dataService = new DataService();
					if (webRequest.Method == "GET")
						{
							HttpWebRequest clonedWebRequest = webRequest.CloneRequestForXSRFToken(webRequest.RequestUri.AbsoluteUri);
							HttpWebResponse webResponse = clonedWebRequest.GetResponse() as HttpWebResponse;
							if (webResponse.StatusCode == HttpStatusCode.OK)
								{
									string response = webResponse.ReadResponse();
									this.AtomDeltaToken = dataService.ReadResponse<AtomDeltaTokens>(response);
								}
						}
				}
			}
		}
	}