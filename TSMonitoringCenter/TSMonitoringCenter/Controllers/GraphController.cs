using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class GraphController : ApiController
    {
        [System.Web.Http.HttpGet]
        public chartData GetAllChartData(string id = "30")
        {
            return Graph.GetAllChartData(id);
        }
    }
}