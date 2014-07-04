using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Net;

namespace AdnWebAPI
{
	#region Categories & Severity Enum

    /// <summary>
    /// Helps enumerate the Activity from where the log is being made
    /// </summary>
    internal enum Categories
    {
		Configuration = 2010,
		ServiceCall = 3010,
        Outlook = 4010,
        Outlook_StarterKit = 4020,
        Outlook_GetAll = 4030,
        Outlook_Create = 4040,
        Outlook_Update = 4050,
        Outlook_Delete = 4060,
		Outlook_RelatedEntity = 4070,
		Outlook_CustomTab = 4080,
		Outlook_WF = 4090,
        Unknown = 5010,
		Excel = 6010,
        Excel_StarterKit = 6020,
        Excel_GetAll = 6030,
        Excel_Create = 6040,
        Excel_Update = 6050,
        Excel_Delete = 6060,
    };

    /// <summary>
    /// Severity of the message being logged
    /// </summary>
    internal enum Severity
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Verbose = 3
    };

    #endregion

	#region Logger Class

    /// <summary>
    /// This class provides api to log the necessary information to eventviewer or local file. The class uses System.Trace api and therefore the logging can be enabled through app.config. For details ref
	/// This class can be extended to support any other custom logging (including third party logs)
    /// </summary>
    static class Logger
    {
		#region Logger Initializer

        /// <summary>
        /// We initialize the logger activitiy id
        /// </summary>
        static Logger()
        {
            GenerateActivityID();
        }

        #endregion

        #region Log Methods 

        internal static System.Diagnostics.TraceSwitch traceLevel = new System.Diagnostics.TraceSwitch("TraceLevel", "Level of messages to log"); 

        /// <summary>
        /// This method would allow the user to log messages to the configured log location
        /// You may choose not to enter any object[] args at the end.
        /// </summary>
        /// <param name="severityInfo">Please set the severity of the message being logged</param>
        /// <param name="categoryType">This would help the user to mention the scenario from where the message is being logged</param>
        /// <param name="logMessage">The actual message that needs to be logged</param>
        /// <param name="args">Place holder for any variable that needs to be logged (Type: object[])</param>
        internal static void Log(Severity severityInfo, Categories categoryType, string logMessage, params object[] args)
        {
            
			if (traceLevel.Level != System.Diagnostics.TraceLevel.Off)
			{
                string formattedLogMessage = GetFormattedMessage(logMessage, categoryType,severityInfo,args);

				switch (severityInfo)
				{
					case Severity.Verbose:
                         if (traceLevel.Level == System.Diagnostics.TraceLevel.Verbose)
                        {
                            Trace.TraceInformation(formattedLogMessage);
                        }
						break;
					case Severity.Info:
						 if (traceLevel.Level == System.Diagnostics.TraceLevel.Info||traceLevel.Level == TraceLevel.Verbose)
                        {
                            Trace.TraceInformation(formattedLogMessage);
                        }
						break;
					case Severity.Error:
                        Trace.TraceError(formattedLogMessage);
						break;
					case Severity.Warning:
						 if (traceLevel.Level == System.Diagnostics.TraceLevel.Verbose || traceLevel.Level == System.Diagnostics.TraceLevel.Info || traceLevel.Level == System.Diagnostics.TraceLevel.Warning)
                        {
                            Trace.TraceWarning(formattedLogMessage);
                        }
						break;
					default:
						break;

				}
		    }
        }

		#endregion

		#region Log Exceptions Handler
        
		/// <summary>
        /// This method will log the exception message
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
		internal static void LogException(Severity severityInfo, Categories categoryType, Exception exception)
        {
            StringBuilder errorBuilder = new StringBuilder();
            GetExceptionMessage(exception, ref errorBuilder);
            if (exception.InnerException != null)
            {
                GetExceptionMessage(exception.InnerException, ref errorBuilder);
            }
            Log(severityInfo, categoryType, errorBuilder.ToString());
        }

        /// <summary>
        /// This method will log the custom message along with the exception traces
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
        /// <param name="customErrorMessage">custom messages</param>
        internal static void LogException(Severity severityInfo, Categories categoryType, string customErrorMessage,Exception exception)
        {
            Logger.Log(severityInfo, categoryType, customErrorMessage);
            Logger.LogException(severityInfo, categoryType, exception);
        }

		/// <summary>
        /// This method will log the exception message
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
        internal static void LogException(Severity severityInfo, Categories categoryType, WebException exception)
        {
            StringBuilder errorBuilder = new StringBuilder();
            GetExceptionMessage(exception, ref errorBuilder);
            if (exception.InnerException != null)
            {
                GetExceptionMessage(exception.InnerException, ref errorBuilder);
            }
            Log(severityInfo, categoryType, errorBuilder.ToString());
        }

        /// <summary>
        /// This method will log the custom message along with the exception traces
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
        /// <param name="customErrorMessage">custom messages</param>
        internal static void LogException(Severity severityInfo, Categories categoryType, string customErrorMessage, WebException exception)
        {
            Logger.Log(severityInfo, categoryType, customErrorMessage);
            Logger.LogException(severityInfo, categoryType, exception);
        }

        /// <summary>
        /// This method will log the exception message
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
        internal static void LogException(Severity severityInfo, Categories categoryType, COMException exception)
        {
            StringBuilder errorBuilder = new StringBuilder();
            GetExceptionMessage(exception, ref errorBuilder);
            if (exception.InnerException != null)
            {
                GetExceptionMessage(exception.InnerException, ref errorBuilder);
            }
            Log(severityInfo, categoryType, errorBuilder.ToString());
        }

        /// <summary>
        /// This method will log the custom message along with the exception traces
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="categoryType">cateogry type - outlook,read,getall, create..</param>
        /// <param name="severityInfo">severity info - error, info,verbose,warning</param>
        /// <param name="customErrorMessage">custom messages</param>
        internal static void LogException(Severity severityInfo, Categories categoryType, string customErrorMessage, COMException exception)
        {
            Logger.Log(severityInfo, categoryType, customErrorMessage);
            Logger.LogException(severityInfo, categoryType, exception);
        }

        #endregion

		#region Activity Id Handling

        public static string ActivityID { get; set; }

        /// <summary>
        /// This method will generate a new guid for the E2E tracing.
        /// </summary>
        public static string GenerateActivityID()
        {
            ActivityID = System.Guid.NewGuid().ToString().Replace("-", "");
            return ActivityID;
        }

        #endregion

        #region Log Helper methods

        /// <summary>
        /// This method will return the formatted message
        /// </summary>
        /// <param name="message">log message</param>
        /// <param name="severity">severity type like info, verbose,error, warning</param>
        /// <param name="categoryType">category type</param>
        /// <param name="args">additional params</param>
        /// <returns>formatted string</returns>
        private static string GetFormattedMessage(string message, Categories categoryType,Severity severity, params object[] args)
        {
            string formattedMessage = message;
            if (args.Length != 0)
            {
                formattedMessage = String.Format(CultureInfo.InvariantCulture, formattedMessage, args);
            }
            formattedMessage = string.Format(CultureInfo.InvariantCulture, "{4} \t ActivityID: {3} \t Category: {1} \t Severity: {2} \t Details: {0}", formattedMessage, categoryType,severity,ActivityID,DateTime.Now);
            return formattedMessage;
        }

		/// <summary>
        /// This method will get the detailed error from the exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorBuilder"></param>
        private static void GetExceptionMessage(Exception exception, ref StringBuilder errorBuilder)
        {
            errorBuilder.Append("Error Source: " + exception.Source);
            errorBuilder.Append("Error Message: " + exception.Message);
            errorBuilder.Append("StackTrace: " + exception.StackTrace);
        }


        /// <summary>
        /// This method will get the detailed error from the com exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorBuilder"></param>
        private static void GetDetailedErrorFromException(COMException exception, ref StringBuilder errorBuilder)
        {
            errorBuilder.Append("HResult: " + exception.ErrorCode);
            GetExceptionMessage(exception, ref errorBuilder);
        }

        /// <summary>
        /// This method will get the detailed error from the WebException exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorBuilder"></param>
        private static void GetDetailedErrorFromException(WebException exception, ref StringBuilder errorBuilder)
        {
            if (exception.Response != null)
            {
                errorBuilder.Append("Status Code: " + ((HttpWebResponse)exception.Response).StatusCode);
            }
            GetExceptionMessage(exception, ref errorBuilder);
        }

        #endregion
    }

	#endregion
}

