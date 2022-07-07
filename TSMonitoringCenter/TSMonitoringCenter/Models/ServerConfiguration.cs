using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter.Models
{
    public class ServerConfiguration
    {
        public string Id { get; set; }
        public string ServerName { get; set; }
        public List<ServerConfigurationItem> ConfigList { get; set; }

        public static ServerConfiguration GetConfig(string Id)
        {
            ServerConfiguration sc = new ServerConfiguration
            {
                Id = Id,
                ConfigList = new List<ServerConfigurationItem>()
            };
            
            DBUtils db = DBUtils.GetInstance();
            string serverName = "";
            foreach (var item in db.GetServerConfig(Id))
            {
                sc.ConfigList.Add(new ServerConfigurationItem
                {
                    Name = item[1],
                    Value = item[2],
                    Description = item[3]
                });
                serverName = item[0];
            }
            sc.ServerName = serverName;
            return sc;
        }

        public static bool UpdateServerConfiguration(ServerConfiguration config)
        {
            //return true;
            //DBUtils db = DBUtils.GetInstance();
            string ServerName = config.ServerName;

            foreach (ServerConfigurationItem item in config.ConfigList)
            {
                if (!DBUtils.GetInstance().UpdateConfigItem(ServerName, item.Name, item.Value))
                {
                    return false;
                }
            }
            if (config.ServerName == "MonitoringCenter")
            {
                Configuration.ReloadConfig();
            }

            return RenewConfig(config.Id);
        }

        public static bool RenewConfig(string Id)
        {
            return ASInfo.SetNotInAutomaticMode(Id) &&
                ASInfo.StopUsing(Id) &&
                ASInfo.SetRenewConfig(Id);
        }

        public static ServerConfiguration GetCenterConfig()
        {
            List<ServerConfigurationItem> configList = new List<ServerConfigurationItem>();
            DBUtils db = DBUtils.GetInstance();
            string serverName = "";
            foreach (var item in db.GetCenterConfig())
            {
                configList.Add(new ServerConfigurationItem
                {
                    Name = item[1],
                    Value = item[2],
                    Description = item[3]
                });
                serverName = item[0];
            }
            ServerConfiguration config = new ServerConfiguration
            {
                ConfigList = configList,
                ServerName = serverName,
                Id = ""
            };
            return config;
        }
    }

    public class ServerConfigurationItem
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string Description { get; set; }
    }
}