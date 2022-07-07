using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter.Models
{
    public class ASInfo
    {
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public int InAutomaticMode { get; private set; }
        public int InUse { get; private set; }
        public int InColdStart { get; private set; }
        public double RAM { get; private set; }
        public double CPU { get; private set; }
        public TimeSpan ProcessWorkTime { get; private set; }
        public string URL { get; private set; }

        public ASInfo(string[] data)
        {
            Guid = data[0];
            Name = data[1];
            InUse = Convert.ToInt32(data[2]);
            InColdStart = Convert.ToInt32(data[3]);
            InAutomaticMode = Convert.ToInt32(data[4]);
            CPU = Convert.ToDouble(data[5]);
            RAM = Convert.ToDouble(data[6]);
            ProcessWorkTime = TimeSpan.Parse(data[7]);
            URL = data[8];
           // CSI."Id",
           //CSI."ServerName",
           //CSI."InUse",
           //CSI."InColdStart",
           //CSI."InAutomaticMode",
           //SC."CPUusage",
           //SC."RAMusage",
           //SC."ProcessWorkingTime",
           //csc."Value" "Url"

        }

        public static List<ASInfo> GetAllServers()
        {
            List<ASInfo> res = new List<ASInfo>();
            DBUtils db = DBUtils.GetInstance();
            foreach (var item in db.GetAllServersData())
            {
                res.Add(new ASInfo(item));
            }
            return res;
        }

        public static bool StopUsing(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.StopUsing(Id);
        }
        public static bool StartUsing(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.StartUsing(Id);
        }
        public static bool SetInAutomaticMode(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.SetInAutomaticMode(Id);
        }
        public static bool SetNotInAutomaticMode(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.SetNotInAutomaticMode(Id);
        }
        public static bool SetRenewConfig(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.SetRenewConfig(Id);
        }

        public static bool ExecuteRestart(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.ExecuteRestart(Id);
        }
        int GetInUseValue(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.GetInUseValue(Id);
        }
        int GetInColdStartValue(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.GetInColdStartValue(Id);
        }
        string GetServerNameValue(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.GetServerNameValue(Id);
        }
        int GetInAutomaticModeValue(string Id)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.GetInAutomaticModeValue(Id);
        }
        string GetServerCapacityValue(string ServerName, string capacity)
        {
            DBUtils db = DBUtils.GetInstance();
            return db.GetServerCapacityValue(ServerName, capacity);
        }
        
    }
}