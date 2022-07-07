using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter
{
    public class DBUtils
    {
        public static DBUtils instance;
        private DbConfiguration dbcofig;
        private OracleConnection con;

        private DBUtils()
        {
            dbcofig = new DbConfiguration();
            CreateConnection();
        }

        public static DBUtils GetInstance()
        {
            if (instance == null)
            {
                instance = new DBUtils();
            }
            return instance;
        }

        protected void CreateConnection()
        {
            con = new OracleConnection();
            OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder
            {
                Password = dbcofig.dbPassword,
                UserID = dbcofig.dbUser,
                DataSource = dbcofig.dataSource
            };
            try
            {
                con.ConnectionString = ocsb.ConnectionString;
                con.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetInUseValue(string Id)
        {
            try
            {
                string s = String.Format("SELECT \"InUse\" FROM \"ColdStartInfo\" WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToInt16(obj);
                }
                catch (Exception)
                {
                    return 0;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int GetInColdStartValue(string Id)
        {
            try
            {
                string s = String.Format("SELECT \"InColdStart\" FROM \"ColdStartInfo\" WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToInt16(obj);
                }
                catch (Exception)
                {
                    return 0;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetInAutomaticModeValue(string Id)
        {
            try
            {
                string s = String.Format("SELECT \"InAutomaticMode\" FROM \"ColdStartInfo\" WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToInt16(obj);
                }
                catch (Exception)
                {
                    return 0;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetServerNameValue(string Id)
        {
            try
            {
                string s = String.Format("SELECT \"ServerName\" FROM \"ColdStartInfo\" WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToString(obj);
                }
                catch (Exception)
                {
                    return null;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetServerCapacityValue(string ServerName, string capacity)
        {
            try
            {
                string s = String.Format("SELECT \"{1}\" FROM \"ServerCapacity\" WHERE \"ServerName\" = '{0}'", ServerName, capacity);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToString(obj);
                }
                catch (Exception)
                {
                    return null;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetServers()
        {
            try
            {
                string s = String.Format("SELECT \"Id\" FROM \"ColdStartInfo\"");
                OracleCommand cmd = new OracleCommand(s, con);
                List<string> result = new List<string>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(Convert.ToString(reader.GetValue(0)));
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string[]> GetAllServersData()
        {
            try
            {
                string s = String.Format("SELECT CSI.\"Id\", " +
                                        "       CSI.\"ServerName\", " +
                                        "       CSI.\"InUse\", " +
                                        "       CSI.\"InColdStart\", " +
                                        "       CSI.\"InAutomaticMode\", " +
                                        "       SC.\"CPUusage\", " +
                                        "       SC.\"RAMusage\", " +
                                        "       SC.\"ProcessWorkingTime\", " +
                                        "       csc.\"Value\" \"Url\" " +
                                        "FROM \"ColdStartInfo\" CSI " +
                                        "INNER JOIN \"ServerCapacity\" SC " +
                                        "  ON CSI.\"ServerName\" = SC.\"ServerName\" " +
                                        "INNER JOIN \"ColdStartConfig\" CSC " +
                                        "  ON CSI.\"ServerName\" = CSC.\"ServerName\" " +
                                        "  AND CSC.\"Name\" = 'addressForLogin' ");
                OracleCommand cmd = new OracleCommand(s, con);
                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[9];

                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            item[4] = Convert.ToString(reader.GetValue(4));
                            item[5] = Convert.ToString(reader.GetValue(5));
                            item[6] = Convert.ToString(reader.GetValue(6));
                            item[7] = Convert.ToString(reader.GetValue(7));
                            item[8] = Convert.ToString(reader.GetValue(8));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool StopUsing(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"InUse\" = 0" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public bool StartUsing(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"InUse\" = 1" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public bool SetInAutomaticMode(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"InAutomaticMode\" = 1" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public bool SetNotInAutomaticMode(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"InAutomaticMode\" = 0" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public bool ExecuteRestart(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"ExecuteRestart\" = 1" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
        public bool SetRenewConfig(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartInfo\"" +
                                            " SET \"RenewConfig\" = 1" +
                                            " WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
        public List<string[]> GetChartData(string SCIid, string interval = "30")
        {
            try
            {
                string s = String.Format("SELECT SCG.\"ColdStartInfoId\", SCG.\"ServerName\", TO_CHAR(SCG.\"CreatedOn\", 'hh24:mi:ss') \"Time\"," +
                                        " SCG.\"RAMusage\", SCG.\"CPUusage\"  FROM \"ServerCapacityGraph\" SCG " +
                                        " WHERE SCG.\"ColdStartInfoId\" = '{0}' " +
                                        " AND SCG.\"CreatedOn\" > SYSDATE - {1}/24/60 " +
                                        " ORDER BY \"CreatedOn\" ASC ", SCIid, interval);
                OracleCommand cmd = new OracleCommand(s, con);
                
                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[5];

                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            item[4] = Convert.ToString(reader.GetValue(4));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string[]> GetAllChartData(string interval = "30")
        {
            //try
            //{
                
                string s = String.Format("SELECT SCG.\"ColdStartInfoId\", SCG.\"ServerName\", TO_CHAR(SCG.\"CreatedOn\", 'hh24:mi:ss') \"Time\"," +
                                        " SCG.\"RAMusage\", SCG.\"CPUusage\" FROM \"ServerCapacityGraph\" SCG " +
                                        " WHERE SCG.\"CreatedOn\" > SYSDATE - {0}/24/60" +
                                        " ORDER BY \"CreatedOn\" ASC ", interval);
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[5];

                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            item[4] = Convert.ToString(reader.GetValue(4));
                            result.Add(item);
                        }
                    }
                }
                return result;
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        public List<string[]> GetUserSessions()
        {
            try
            {
                string s = String.Format("SELECT c.\"Name\", COUNT(sus.\"SysUserId\")"
                                          + "  FROM \"SysUserSession\" sus"
                                          + "  INNER JOIN \"SysAdminUnit\" sau"
                                          + "  INNER JOIN \"Contact\" c"
                                          + "      ON sau.\"ContactId\" = c.\"Id\""
                                          + "      ON sus.\"SysUserId\" = sau.\"Id\""
                                          + "  WHERE sus.\"SessionEndDate\" IS NULL"
                                          + "  GROUP BY sus.\"SysUserId\", c.\"Name\", sau.\"Name\""
                                          + "  ORDER BY 2 DESC");
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[2];

                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CountActiveUsers()
        {
            try
            {
                string s = String.Format("SELECT COUNT(DISTINCT sus.\"SysUserId\") " +
                                        "FROM \"SysUserSession\" sus " +
                                        "WHERE sus.\"SessionEndDate\" IS NULL");
                OracleCommand cmd = new OracleCommand(s, con);
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string[]> GetDBBlock()
        {
            try
            {
                string s = String.Format("SELECT DISTINCT LEVEL, " +
                                         "AO.OBJECT_NAME, " +
                                         "b.username, " +
                                         "b.status, " +
                                         "B.OSUSER, " +
                                         "B.MACHINE, " +
                                         "B.MODULE, " +
                                         "B.SECONDS_IN_WAIT, " +
                                         "A.SQL_EXEC_START " +
                                         "FROM v$session a, " +
                                         "     v$session b, " +
                                         "     ALL_OBJECTS AO " +
                                         "WHERE a.blocking_session = b.sid " +
                                         "  AND A.ROW_WAIT_OBJ# = AO.OBJECT_ID " +
                                         "CONNECT BY PRIOR a.sid = b.sid " +
                                         "START WITH b.BLOCKING_SESSION IS NULL " +
                                         "ORDER BY LEVEL");
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[9];
                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            item[4] = Convert.ToString(reader.GetValue(4));
                            item[5] = Convert.ToString(reader.GetValue(5));
                            item[6] = Convert.ToString(reader.GetValue(6));
                            item[7] = Convert.ToString(reader.GetValue(7));
                            item[8] = Convert.ToString(reader.GetValue(8));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string[]> GetServerConfig(string Id)
        {
            try
            {
                string s = String.Format("SELECT CSC.\"ServerName\", "+
                                        "        CSC.\"Name\", "+
                                        "        CSC.\"Value\", "+
                                        "        CSC.\"Description\" "+
                                        "FROM \"ColdStartInfo\" CSI "+
                                        "INNER JOIN \"ColdStartConfig\" CSC "+
                                        "    ON CSI.\"ServerName\" = CSC.\"ServerName\" "+
                                        "WHERE CSI.\"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[4];
                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<string[]> GetCenterConfig(string name = "MonitoringCenter")
        {
            try
            {
                string s = String.Format("SELECT CSC.\"ServerName\", "+
                                          "CSC.\"Name\", " +
                                          "CSC.\"Value\", " +
                                          "CSC.\"Description\" " +
                                    "FROM \"ColdStartConfig\" CSC " +
                                    "WHERE CSC.\"ServerName\" = '{0}'", name);
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[4];
                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool UpdateConfigItem(string ServerName, string PropertyName, string Value)
        {
            try
            {
                string s = String.Format("UPDATE \"ColdStartConfig\" CSC " +
                                        "SET CSC.\"Value\" = '{2}' "+
                                        "WHERE CSC.\"ServerName\" = '{0}' AND CSC.\"Name\" = '{1}'", ServerName, PropertyName, Value);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public string GetConfigParameter(string name)
        {
            try
            {
                string s = String.Format("SELECT \"Value\" FROM \"ColdStartConfig\" WHERE \"ServerName\" = '{0}' AND \"Name\" = '{1}'", "MonitoringCenter", name);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                try
                {
                    return Convert.ToString(obj);
                }
                catch (Exception)
                {
                    return null;
                }
                //return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int SetConfigParameter(string name, string value, string description)
        {
            try
            {
                string s = String.Format("SELECT \"Id\" FROM \"ColdStartConfig\" WHERE \"ServerName\" = '{0}' AND \"Name\" = '{1}'", "MonitoringCenter", name);
                OracleCommand cmd = new OracleCommand(s, con);
                object obj = cmd.ExecuteScalar();
                if (Convert.ToString(obj) == null || Convert.ToString(obj) == "")
                {
                    s = String.Format("INSERT INTO \"ColdStartConfig\"(\"CreatedOn\", \"ModifiedBy\", \"ModifiedOn\", \"ServerName\", \"Name\", \"Value\", \"Description\") " +
                                              "VALUES('{0}', 'SERVICE', '{0}', '{1}', '{2}', '{3}', '{4}')", DateTime.Now, "MonitoringCenter", name, value, description);
                    cmd = new OracleCommand(s, con);
                    return cmd.ExecuteNonQuery();
                }
                else
                {
                    s = String.Format("UPDATE \"ServerCapacity\"" +
                                            " SET \"Value\" = '{0}'," +
                                            "\"Description\" = '{1}'" +
                                            " WHERE \"Id\" = '{2}'", value, description, Convert.ToString(obj));
                    cmd = new OracleCommand(s, con);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string[]> GetSessionsToKill()
        {
            try
            {
                string s = String.Format("SELECT SUS.\"Id\", " +
                                        "       c.\"Name\", " +
                                        "       sau.\"Name\" \"Login\", " +
                                        "       SUS.\"SessionStartDate\", " +
                                        "       SUS.\"SessionId\" " +
                                        "FROM \"SysUserSession\" SUS " +
                                        "INNER JOIN \"SysAdminUnit\" SAU " +
                                        "  ON SUS.\"SysUserId\" = SAU.\"Id\" " +
                                        "INNER JOIN \"Contact\" C " +
                                        "  ON SAU.\"ContactId\" = C.\"Id\" " +
                                        "WHERE SUS.\"SessionEndDate\" IS NULL " +
                                        "  AND SAU.\"Name\" NOT IN ('SDH', 'Supervisor') " +
                                        "  AND SUS.\"Id\" NOT IN (SELECT SUS.\"Id\" " +
                                        "            FROM \"SysUserSession\" SUS " +
                                        "            INNER JOIN (SELECT SUS.\"SysUserId\" \"UserId\", " +
                                        "                               MAX(SUS.\"SessionStartDate\") \"Max_date\" " +
                                        "                      FROM \"SysUserSession\" sus " +
                                        "                      WHERE sus.\"SessionEndDate\" IS NULL " +
                                        "                      GROUP BY \"SysUserId\") qq " +
                                        "              ON QQ.\"UserId\" = SUS.\"SysUserId\" " +
                                        "              AND QQ.\"Max_date\" = SUS.\"SessionStartDate\") " +
                                        "ORDER BY \"Login\" DESC");
                OracleCommand cmd = new OracleCommand(s, con);

                List<string[]> result = new List<string[]>();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] item = new string[9];
                            item[0] = Convert.ToString(reader.GetValue(0));
                            item[1] = Convert.ToString(reader.GetValue(1));
                            item[2] = Convert.ToString(reader.GetValue(2));
                            item[3] = Convert.ToString(reader.GetValue(3));
                            item[4] = Convert.ToString(reader.GetValue(4));
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateSession(string Id)
        {
            try
            {
                string s = String.Format("UPDATE \"SysUserSession\" " +
                                         "SET \"SessionEndDate\" = SYSDATE-3/24" +
                                          "WHERE \"Id\" = '{0}'", Id);
                OracleCommand cmd = new OracleCommand(s, con);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
    }
}