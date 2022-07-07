using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class RedisManagerController : ApiController
    {
        [HttpGet]
        public int CloseDoubleSessions()
        {
            return RedisManager.CloseSessions();
        }
    }
}
