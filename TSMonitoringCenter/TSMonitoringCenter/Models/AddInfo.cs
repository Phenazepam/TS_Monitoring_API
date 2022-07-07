using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TSMonitoringCenter.Models
{
    public class AddInfo
    {
        public static List<DBBlockInfo> GetDBBlock()
        {
            List<DBBlockInfo> res = new List<DBBlockInfo>();
            DBUtils db = DBUtils.GetInstance();
            int cnt = 0;

            foreach (var item in db.GetDBBlock())
            {
                res.Add(new DBBlockInfo() {
                    Id = cnt,
                    ObjectName = item[1],
                    UserName = item[2],
                    Status = item[3],
                    OsUser = item[4],
                    Machine = item[5],
                    Module = item[6],
                    SecondsInWait = Convert.ToInt32(item[7]),
                    SQLExecStart = Convert.ToDateTime(item[8])
                });
                cnt++;
            }
            return res;
        }
    }

    public class DBBlockInfo
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string OsUser { get; set; }
        public string Machine { get; set; }
        public string Module { get; set; }
        public int SecondsInWait { get; set; }
        public DateTime SQLExecStart { get; set; }
    }
}