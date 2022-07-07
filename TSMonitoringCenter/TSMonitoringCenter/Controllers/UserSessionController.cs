using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class UserSessionController : ApiController
    {
        public List<UserSession> GetUserSessionsList()
        {
            return UserSession.GetUserSessionsList();
        }
        public int GetCountActiveUsers()
        {
            return UserSession.CountActiveUsers();
        }
    }
}
