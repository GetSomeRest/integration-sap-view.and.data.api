using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using SAP.IW.SSO;
using System.Globalization;
using SAP.IW.GWPAM.Common.Configuration;
using System.Web;

namespace AdnWebAPI.BusinessEntity
{
	/// <summary>
/// This class handles the connectivity to the oData endpoint.  
/// It performs the required handling to enable authentication to the SAP NetWeaver Gateway endpoint. 
/// It also decorates the call to the SAP NetWeaver Gateway endpoint with requestId and SAP Passport required for Enterprise readiness
/// </summary>
    static class BusinessConnectivityHelper
    {
		/// <summary>
        /// This property should be set with the user identity in case the X509 SSO from a server
        /// </summary>
        public static string UserContext { get; set; }

		#region Function import helper

        /// <summary>
        /// This method will call the function import call for the provided service detail, function name and parameter list
        /// </summary>
        /// <param name="serviceDetail">serviceDetail object</param>
        /// <param name="functionName">name of the fuction import</param>
        /// <param name="paramList">url parameter list for the key properties</param>
        /// <param name="methodType">type - POST</param>
        /// <returns>http web response</returns>
        internal static HttpWebResponse CallFunctionImport(ServiceDetails serviceDetail, string functionName, List<UrlParam> paramList, string methodType = "POST")
        {
			Logger.Log(Severity.Info, Categories.ServiceCall, "Triggering service operation call "+ functionName);
            string serviceUrl = GetFunctionImportUrl(serviceDetail.Url, functionName, paramList);
            HttpWebRequest request = CreateNewRequest(new Uri(serviceUrl), serviceDetail, methodType);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			SAP.IW.SSO.Supportability.SingleActivityTraceUtil.Instance.UpdateRequestForSAT(GetHttpResponseHeaders(response), (int)response.StatusCode, response.ResponseUri.ToString());
            return response;
        }

		/// <summary>
        /// This method returns the header of the HttpWebResponse
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetHttpResponseHeaders(WebResponse response)
        {
            Dictionary<string,string> headers = new Dictionary<string,string>();
            foreach (string header in response.Headers)
            {
                headers.Add(header, response.Headers[header]);
            }
            
            return headers;
        }

        /// <summary>
        /// This method will get the function import url for the provided function import and url
        /// </summary>
        /// <param name="baseUrl">absolute url of the service</param>
        /// <param name="functionName">name of the function import</param>
        /// <param name="paramList">url parameter list for the key properties</param>
        /// <returns>http web response</returns>
        internal static string GetFunctionImportUrl(string baseUrl, string functionName, List<UrlParam> paramList)
        {
			Logger.Log(Severity.Verbose, Categories.ServiceCall, "BusinessConnectivityHelper::GetFunctionImportUrl(functionName={0})",functionName);
            string serviceUrl = string.Concat(baseUrl.TrimEnd('/'), string.Format(CultureInfo.InvariantCulture,"/{0}?", functionName));
            string formattedString = string.Empty;
            foreach (var item in paramList)
            {
                formattedString += item.GetFormmattedParamValue();
            }
            serviceUrl = string.Concat(serviceUrl, formattedString.TrimEnd('&'));
            return serviceUrl;
        }

        /// <summary>
        /// This method will return the Read item url based on the baseurl, collection name and the url params list
        /// </summary>
        /// <param name="baseUrl">base url of the service</param>
        /// <param name="collectionName">collection name</param>
        /// <param name="paramList">url parameter list for the key properties</param>
        /// <returns>url</returns>
        internal static string GetReadItemUrl(string baseUrl,string collectionName, List<UrlParam> paramList)
        {
			Logger.Log(Severity.Verbose, Categories.ServiceCall, "BusinessConnectivityHelper::GetReadItemUrl(collectionName={0})",collectionName);
			string serviceUrl = string.Concat(baseUrl.TrimEnd('/'), string.Format(CultureInfo.InvariantCulture,"/{0}(", collectionName));
            string formattedString = string.Empty;
            foreach (var item in paramList)
            {
                formattedString += item.GetFormmattedParamValue();
                formattedString = formattedString.Replace("&", ",");
            }
            serviceUrl = string.Concat(serviceUrl, formattedString.TrimEnd(','));
            serviceUrl = string.Concat(serviceUrl, ")");
            return serviceUrl;
        }

        /// <summary>
        /// This method will prepare the http web request for the SAP connectivity and will handle SSO, E2E tracing and SAP Passport handling
        /// </summary>
        /// <param name="serviceDetail">Service Details</param>
        /// <param name="webRequest">Web Request</param>
        internal static void HandleSAPConnectivity(ServiceDetails serviceDetail,ref HttpWebRequest webRequest)
        {
		    string requestID="";
			Logger.Log(Severity.Verbose, Categories.ServiceCall, "BusinessConnectivityHelper::HandleSAPConnectivity()");
            AuthenticationType authenticationType = GetAuthenticationType(serviceDetail.SSO);
            webRequest.Headers["sap-client"] = serviceDetail.Client;
            requestID=webRequest.Headers["RequestID"] = SAP.IW.SSO.Supportability.E2ETraceUtil.GetRequestId();
			Logger.Log(Severity.Info, Categories.ServiceCall, "Setting Request Id : " + requestID);
            SAP.IW.SSO.Supportability.SingleActivityTraceUtil.Instance.PrepareRequestForSAT(ref webRequest);

			Logger.Log(Severity.Info, 
                Categories.Outlook, 
                "Selected authentication type " + authenticationType);

            ISSOProvider ssoProvider = SSOProviderFactory.Instance.GetSSOProvider(
                authenticationType,
                webRequest.Method,
                "",
                "",
                serviceDetail.RootCASubjectName,
                true);
	
			switch(authenticationType)
			{
				case AuthenticationType.BASIC:
                    
                    webRequest.Credentials = new System.Net.NetworkCredential(
                        "Your SAP Login Here...",
                        "Your SAP Password Here...");
					
                        break;
			}

			ssoProvider.SAPCredientials(ref webRequest);
        }

        #endregion 

        #region Private Helper 

        /// <summary>
        /// This method will create the new http web request with SSO and E2E tracing handled
        /// </summary>
        /// <param name="uri">end point</param>
        /// <param name="serviceDetail">service details object</param>
        /// <param name="methodType">http method</param>
        /// <returns>http web request</returns>
        internal static HttpWebRequest CreateNewRequest(Uri uri, ServiceDetails serviceDetail, string methodType = "POST")
        {
            HttpWebRequest webRequest = WebRequest.Create(uri) as HttpWebRequest;
            webRequest.Method = methodType;
            HandleSAPConnectivity(serviceDetail, ref webRequest);
            return webRequest;
        }

						 /// <summary>
				/// This method will return the current logged on user context from the Http headears. This method should be changed in case the
				/// the subject name required for the user certificate is different.
				/// </summary>
				/// <returns>current logged on user name</returns>
				internal static string GetUserContext()
				{
					return HttpContext.Current.User.Identity.Name;
				}
			
		

        /// <summary>
        /// This method will get the authentication type
        /// </summary>
        /// <param name="authenticationString">authentication type["BASIC","X509","SAML20"]</param>
        /// <returns>authentication type - BASIC,SAML20,X509</returns>
        private static AuthenticationType GetAuthenticationType(string authenticationString)
        {
            AuthenticationType authenticationType = AuthenticationType.BASIC;
            if (Enum.IsDefined(typeof(AuthenticationType), authenticationString))
                authenticationType = (AuthenticationType)Enum.Parse(typeof(AuthenticationType), authenticationString, true);
            return authenticationType;
        }

        #endregion
    }

	///<summary>
	///This class contains the methods for formatting the parmater values to construct an url
	///</summary>
    internal class UrlParam
    {
        #region Function Param property

        internal string ParamName { get; set; }
        internal dynamic ParamValue { get; set; }
        internal bool IsBinaryParam { get; set; }

        #endregion 

        #region Helper methods

        internal UrlParam() { this.IsBinaryParam = false; }

        /// <summary>
        /// This method will return the formatted parameter values
        /// </summary>
        /// <returns>formatted value of the parameter</returns>
        internal string GetFormmattedParamValue()
        {
            string formmatedParamValue = string.Concat(this.ParamName, "=");

            if (ParamValue.GetType() == typeof(string))
            {
                if (this.IsBinaryParam == false)
                {
                    formmatedParamValue += string.Format("'{0}'&", this.ParamValue); 
                }
                else
                {
                    formmatedParamValue += string.Format("binary'{0}'&", this.ParamValue); 
                }
            }
            else if (ParamValue.GetType() == typeof(DateTime))
            {
                formmatedParamValue += string.Format("datetime'{0}'&", this.ParamValue);
            }
            else if (ParamValue.GetType() == typeof(Guid))
            {
                formmatedParamValue += string.Format("guid'{0}'&", this.ParamValue);
            }
            else if (ParamValue.GetType() == typeof(DateTimeOffset))
            {
                formmatedParamValue += string.Format("{0}&", this.ParamValue);
            }
			else if (ParamValue.GetType() == typeof(Int32))
            {
                formmatedParamValue += string.Format("{0}&", this.ParamValue);
            }
            return formmatedParamValue;
        }

        #endregion

    }
}
