using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter.Models
{
    public class chartData
    {
        // string[] labels { get; set; }
        public RAM ram{ get; set; }
        public CPU cpu { get; set; }

    }

    public class RAM
    {
        public List<datasets> datasets { get; set; }
        public List<string> labels = new List<string>();
    }
    public class CPU
    {
        public List<datasets> datasets { get; set; }
        public List<string> labels = new List<string>();
    }
    public class datasets
    {
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public List<data> data { get; set; }
        public double pointRadius { get; set; }
        public double borderWidth { get; set; }

    }
    public class data
    {
        public string x { get; set; }
        public double y { get; set; }
        public data(string x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Graph
    {
        public chartData chartData = new chartData();
        // public List<string> labels = new List<string>();

        static string[] colors =
        {
            "red",
            "green",
            "blue",
            "yellow",
            "magenta",
            "aqua",        
        };

        //public static chartData GetAllChartData()
        //{
        //    chartData result = new chartData();
        //    result.ram = new RAM();
        //    result.cpu = new CPU();


        //    result.ram.datasets = new List<datasets>();
        //    result.cpu.datasets = new List<datasets>();
        //    int c = 0;
        //    DBUtils db = DBUtils.GetInstance();
        //    try
        //    {
        //        foreach(var serverId in db.GetServers())
        //        {
        //            List<string[]> tmp = db.GetChartData(serverId);
        //            if (tmp.Count != 0)
        //            {
        //                datasets ds_ram = new datasets();
        //                ds_ram.label = tmp[0][1];
        //                ds_ram.borderColor = colors[c];
        //                ds_ram.backgroundColor = colors[c];
        //                ds_ram.pointRadius = 1;
        //                ds_ram.borderWidth = 1;
        //                ds_ram.data = new List<double>();

        //                datasets ds_cpu = new datasets();
        //                ds_cpu.label = tmp[0][1];
        //                ds_cpu.borderColor = colors[c];
        //                ds_cpu.backgroundColor = colors[c];
        //                ds_cpu.pointRadius = 1;
        //                ds_cpu.borderWidth = 1;
        //                ds_cpu.data = new List<double>();

        //                c++;
        //                foreach (var item in tmp)
        //                {
        //                    result.ram.labels.Add((item[2]));
        //                    ds_ram.data.Add(Convert.ToDouble(item[3]));
        //                    ds_cpu.data.Add(Convert.ToDouble(item[4]));
        //                }
        //                result.ram.datasets.Add(ds_ram);
        //                result.cpu.datasets.Add(ds_cpu);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
            
        //    return result;
        //}


        public static chartData GetAllChartData(string interval)
        {
            chartData result = new chartData
            {
                ram = new RAM(),
                cpu = new CPU()
            };


            result.ram.datasets = new List<datasets>();
            result.cpu.datasets = new List<datasets>();
            int c = 0;
            int c1 = 0;
            DBUtils db = DBUtils.GetInstance();
            try
            {
                List<string[]> tmp = db.GetAllChartData(interval);
                if (tmp.Count != 0)
                {
                    foreach (var item in tmp)
                    {
                        if (!result.ram.labels.Exists(x => x == item[2]))
                        {
                            result.ram.labels.Add((item[2]));
                        }
                        if (!result.cpu.labels.Exists(x => x == item[2]))
                        {
                            result.cpu.labels.Add((item[2]));
                        }
                        datasets object_ram = new datasets
                        {
                            label = item[1],
                            borderColor = colors[c],
                            backgroundColor = colors[c],
                            pointRadius = 1,
                            borderWidth = 1,
                            data = new List<data>()
                        };
                        datasets object_cpu = new datasets
                        {
                            label = item[1],
                            borderColor = colors[c1],
                            backgroundColor = colors[c1],
                            pointRadius = 1,
                            borderWidth = 1,
                            data = new List<data>()
                        };

                        if (result.ram.datasets.Exists(x => x.label == object_ram.label))
                        {
                            result.ram.datasets.Find(x => x.label == object_ram.label).data.Add(new data(item[2],Convert.ToDouble(item[3])));
                        }
                        else
                        {
                            result.ram.datasets.Add(object_ram);
                            result.ram.datasets.Find(x => x.label == object_ram.label).data.Add(new data(item[2], Convert.ToDouble(item[3])));
                            c++;
                        }

                        if (result.cpu.datasets.Exists(x => x.label == object_cpu.label))
                        {
                            result.cpu.datasets.Find(x => x.label == object_cpu.label).data.Add(new data(item[2], Convert.ToDouble(item[4])));
                        }
                        else
                        {
                            result.cpu.datasets.Add(object_cpu);
                            result.cpu.datasets.Find(x => x.label == object_cpu.label).data.Add(new data(item[2], Convert.ToDouble(item[4])));
                            c1++;
                        }
                    }

                    //foreach (var item in result.ram.datasets)
                    //{
                    //    if (true)
                    //    {

                    //    }
                    //}

                    //datasets ds_cpu = new datasets
                    //{
                    //    label = tmp[0][1],
                    //    borderColor = colors[c],
                    //    backgroundColor = colors[c],
                    //    pointRadius = 1,
                    //    borderWidth = 1,
                    //    data = new List<double>()
                    //};

                    
                    //foreach (var item in tmp)
                    //{
                    //    result.ram.labels.Add((item[2]));
                    //    ds_ram.data.Add(Convert.ToDouble(item[3]));
                    //    //ds_cpu.data.Add(Convert.ToDouble(item[4]));
                    //}
                    //result.ram.datasets.Add(ds_ram);
                    //result.cpu.datasets.Add(ds_cpu);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }


}