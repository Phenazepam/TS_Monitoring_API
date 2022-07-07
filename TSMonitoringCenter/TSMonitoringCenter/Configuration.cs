using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TSMonitoringCenter
{
    public static class DefaultConfiguration
    {
        public static string[] RedisConnectionString { get; set; } = { "Null", "Адрес для подключения к Redis" };
        public static string[] RedisDBNumber { get; set; } = { "0", "Номер базы данных Redis" };
        public static string[] ASCardUpdateFreq { get; set; } = { "3", "Частота обновления карточки стенда (в сек.)" };
        public static string[] ChartUpdateFreq { get; set; } = { "10", "Частота обновления графика нагрузки (в сек.)" };
        public static string[] DBBlockUpdateFreq { get; set; } = { "5", "Частота обновления инфо о блокировках (в сек.)" };
        public static string[] UserCountUpdateFreq { get; set; } = { "10", "Частота обновления инфо о количестве пользователей (в сек.)" };
        public static string[] UserSessionUpdateFreq { get; set; } = { "5", "Частота обновления инфо о сессиях пользователей (в сек.)" };
        public static string[] ChartTimeDisplay { get; set; } = { "30", "Продолжительность по времени данных в графике (в мин.)" };

        public static string GetValue(string propertyName)
        {
            Type DConf = typeof(DefaultConfiguration);
            PropertyInfo property = DConf.GetProperty(propertyName);
            string[] value = (string[])property.GetValue(propertyName);
            return value[0];
        }

        public static string GetDescription(string propertyName)
        {
            Type DConf = typeof(DefaultConfiguration);
            PropertyInfo property = DConf.GetProperty(propertyName);
            string[] value = (string[])property.GetValue(propertyName);
            return value[1];
        }
    }
    public class Configuration
    {
        public static Configuration instance;
        public string RedisConnectionString { get; set; }
        public string RedisDBNumber { get; set; }
        public string ASCardUpdateFreq { get; set; }
        public string ChartUpdateFreq { get; set; }
        public string DBBlockUpdateFreq { get; set; }
        public string UserCountUpdateFreq { get; set; }
        public string UserSessionUpdateFreq { get; set; }
        public string ChartTimeDisplay { get; set; }

        private Configuration()
        {
            
        }

        public static Configuration GetInstance()
        {
            if (instance == null)
            {
                instance = new Configuration();
                instance = Configuration.SetConfig(instance);
            }
            return instance;
        }

        public static Configuration SetConfig(Configuration instance)
        {
            instance.RedisConnectionString = GetConfigFromDb("RedisConnectionString");
            instance.RedisDBNumber = GetConfigFromDb("RedisDBNumber");
            instance.ASCardUpdateFreq = GetConfigFromDb("ASCardUpdateFreq");
            instance.ChartUpdateFreq = GetConfigFromDb("ChartUpdateFreq");
            instance.DBBlockUpdateFreq = GetConfigFromDb("DBBlockUpdateFreq");
            instance.UserCountUpdateFreq = GetConfigFromDb("UserCountUpdateFreq");
            instance.UserSessionUpdateFreq = GetConfigFromDb("UserSessionUpdateFreq");
            instance.ChartTimeDisplay = GetConfigFromDb("ChartTimeDisplay");
            
            return instance;
        }

        public static void ReloadConfig()
        {
            instance = SetConfig(instance);
        }

        public static string GetConfigFromDb(string name)
        {
            DBUtils db = DBUtils.GetInstance();
            var value = db.GetConfigParameter(name);
            if (/*value != null &&*/ value != "")
            {
                return value;
            }
            else
            {
                value = DefaultConfiguration.GetValue(name);
                string desc = DefaultConfiguration.GetDescription(name);
                db.SetConfigParameter(name, value, desc);
                return value;
                //return (string)GetType(DefaultConfiguration).GetProperty(name).GetValue(name);
            }
        }
    }
    public class DbConfiguration
    {
        public string dataSource { get; set; }
        public string dbUser { get; set; }
        public string dbPassword { get; set; }
        public DbConfiguration()
        {
            dataSource = System.Configuration.ConfigurationSettings.AppSettings["dataSource"];
            dbUser = System.Configuration.ConfigurationSettings.AppSettings["dbUser"];
            dbPassword = System.Configuration.ConfigurationSettings.AppSettings["dbPassword"];
        }
    }
}