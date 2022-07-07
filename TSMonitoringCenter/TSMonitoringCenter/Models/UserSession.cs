using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter.Models
{
    public class UserSession
    {
        public string UserName {get; set;}
        public int SessionCount { get; set; }

        public UserSession(string UserName, int SessionCount)
        {
            this.UserName = UserName;
            this.SessionCount = SessionCount;
        }

        public static List<UserSession> GetUserSessionsList()
        {
            List<UserSession> res = new List<UserSession>();
            DBUtils db = DBUtils.GetInstance();

            foreach (var item in db.GetUserSessions())
            {
                res.Add(new UserSession(item[0], Convert.ToInt32(item[1])));
            }

            return res;
        }

        public static int CountActiveUsers()
        {
            DBUtils db = DBUtils.GetInstance();

            return db.CountActiveUsers();
        }
    }
}