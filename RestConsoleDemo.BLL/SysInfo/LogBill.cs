using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
     public static  class LogBill
    {
        public static string GetAllLogList()
        {
            string str = string.Empty;
            List<CascaderNullModel> tempList = new List<CascaderNullModel>();
            try
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                List<string> strList = new List<string>();
                strList = LoadLogList();
                if (strList != null || strList.Count > 0)
                {
                    foreach (string st in strList)
                    {
                        // string labs = st.Replace("","");
                        CascaderNullModel temp = new CascaderNullModel()
                        {
                            label = st,
                            value = st

                        };
                        tempList.Add(temp);
                    }
                    tempList = tempList.OrderByDescending(p => p.value).ToList();
                    str = JsonConvert.SerializeObject(tempList, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);

                }
                else
                {
                    str = JsonConvert.SerializeObject(tempList, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无日志信息", str);
                }

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
            return str;
        }
        private static string GetAppDataPath()
        {

            string AppDataPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Logs";
            return AppDataPath;
        }
        private static List<string> LoadLogList()
        {
            List<string> listTemp = new List<string>();
            string AppDataPath = GetAppDataPath();
            DirectoryInfo folder = new DirectoryInfo(AppDataPath);

            foreach (FileInfo file in folder.GetFiles("*.log"))
            {
                listTemp.Add(file.Name);
            }

            return listTemp;
        }
        public static string LogRead(string fileName)
        {
            string str = string.Empty;
            try
            {
                string data = string.Empty;
                string AppDataPath = GetAppDataPath();
                if (!Directory.Exists(AppDataPath))
                {
                    // DirectoryInfo floder = Directory.CreateDirectory(AppDataPath);
                    str = ResponseHelper.ResponseMsg("-1", "无日志信息", str);
                    return str;

                }
                string filepath = string.Concat(AppDataPath, "\\", fileName);
                if (!File.Exists(filepath))
                {
                    str = ResponseHelper.ResponseMsg("-1", "无日志信息", str);
                    return str;
                }

                FileStream aFile = new FileStream(filepath, FileMode.Open);

                StreamReader sr = new StreamReader(aFile, Encoding.GetEncoding("gb2312"), true);
                List<LogModel> strList = new List<LogModel>();
                string strReadline;
                while ((strReadline = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(strReadline))
                    {
                        string[] lists = strReadline.Split('|');
                        if (lists.Length == 4)
                        {
                            LogModel temp = new LogModel()
                            {
                                LogDateTime = lists[0],
                                RunClassName = lists[1],
                                LogType = lists[2],
                                LogInfo = lists[3]
                            };
                            strList.Add(temp);
                        }
                        else
                        {
                            LogModel temp = new LogModel()
                            {

                                LogInfo = strReadline
                            };
                            strList.Add(temp);
                        }
                    }
                }
                aFile.Close();
                strList = strList.OrderByDescending(p => p.LogDateTime).ToList();
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                str = JsonConvert.SerializeObject(strList, Formatting.Indented, timeFormat);
                str = ResponseHelper.ResponseMsg("1", "取数成功", str);

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }
            return str;

        }
    }
}
