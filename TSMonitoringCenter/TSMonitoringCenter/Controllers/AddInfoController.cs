using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class AddInfoController : ApiController
    {
        public List<DBBlockInfo> GetDBBlock()
        {
            return AddInfo.GetDBBlock();
        }
    }
}
