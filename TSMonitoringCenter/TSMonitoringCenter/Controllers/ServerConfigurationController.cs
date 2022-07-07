using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class ServerConfigurationController : ApiController
    {
        public ServerConfiguration GetConfig(string Id)
        {
            return ServerConfiguration.GetConfig(Id);
        }

        [HttpOptions, HttpPost]
        public HttpResponseMessage UpdateServerConfiguration([FromBody]ServerConfiguration config)
        {
            try
            {
                ServerConfiguration.UpdateServerConfiguration(config);
                var message = Request.CreateResponse(HttpStatusCode.OK, config.ServerName);
                message.Headers.Location = new Uri(Request.RequestUri + config.Id);
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public ServerConfiguration GetCenterConfig()
        {
            return ServerConfiguration.GetCenterConfig();
        }



    }
}
