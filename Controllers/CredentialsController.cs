using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autodesk.ADN.Toolkit.ViewData;

namespace AdnWebAPI.Controllers
{
    public class CredentialsController : ApiController
    {
        AdnViewDataClient _viewDataClient;

        CredentialsController()
        {
            _viewDataClient = new AdnViewDataClient(
                UserSettings.BASE_URL,
                UserSettings.CONSUMER_KEY,
                UserSettings.CONSUMER_SECRET);
        }

        public async Task<string> Get()
        {
            var tokenResult = await _viewDataClient.AuthenticateAsync();

            if (!tokenResult.IsOk())
            {
                return string.Empty;
            }

            return tokenResult.AccessToken;
        }
    }
}
