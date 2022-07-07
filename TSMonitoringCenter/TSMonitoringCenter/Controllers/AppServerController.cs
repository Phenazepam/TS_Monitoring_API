using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TSMonitoringCenter.Models;

namespace TSMonitoringCenter.Controllers
{
    public class AppServerController : ApiController
    {
        //public ASInfo GetInfo(string id)
        //{
        //    return new ASInfo(id);
        //}

        public List<ASInfo> GetAllServers()
        {
            return ASInfo.GetAllServers();
        }

        [System.Web.Http.HttpGet]
        public bool StopUsing(string id)
        {
            return ASInfo.StopUsing(id);
        }

        [System.Web.Http.HttpGet]
        public bool StartUsing(string id)
        {
            return ASInfo.StartUsing(id);
        }

        [System.Web.Http.HttpGet]
        public bool SetNotInAutomaticMode(string id)
        {
            return ASInfo.SetNotInAutomaticMode(id);
        }

        [System.Web.Http.HttpGet]
        public bool SetInAutomaticMode(string id)
        {
            return ASInfo.SetInAutomaticMode(id);
        }

        [System.Web.Http.HttpGet]
        public bool ExecuteRestart(string id)
        {
            return ASInfo.ExecuteRestart(id);
        }
    }
}