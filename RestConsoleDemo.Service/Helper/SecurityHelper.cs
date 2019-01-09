using NLog;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service.Helper
{
    public static class SecurityHelper
    {
        public static bool Analysis(string Address)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                string BlackCount = System.Configuration.ConfigurationManager.AppSettings["BlackCount"];
                int blackCount = string.IsNullOrEmpty(BlackCount) ? 0 : Convert.ToInt32(BlackCount);
                if(!string.IsNullOrEmpty(Address))
                {
                    if(FindBlackList(Address))
                    {
                        logger.Log(LogLevel.Error, string.Format("{0}为黑名单地址禁止访问", Address));
                        return false;
                    }
                    List<IPCountInfoModel> ipList = DataCacheHelper.GetData<IPCountInfoModel>();
                   
                    int fondCount = 0;
                    if(ipList!=null&& ipList.Count>0)
                    {
                        for(int i=0;i< ipList.Count;i++)
                        {
                            if(ipList[i].Address== Address)
                            {
                                ipList[i].Count = ipList[i].Count + 1;
                                fondCount = ipList[i].Count;
                                break;
                            }
                        }

                    }
                    else
                    {
                        IPCountInfoModel temp = new IPCountInfoModel()
                        {
                            Count = 1,
                            Address = Address

                        };
                        ipList.Add(temp);
                    }
                    DataCacheHelper.Updata<IPCountInfoModel>(ipList);
                    if (fondCount > blackCount)
                    {
                        return false;
                    }


                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Info, string.Format("检索访问缓存异常:{0}", ex.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查找黑名单
        /// </summary>
        /// <param name="Address"></param>
        /// <returns>true找到</returns>
        public static bool FindBlackList(string Address)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
             

                List<tbBlackList> backList = DataCacheHelper.GetData<tbBlackList>();
                if(backList == null|| backList.Count<=0)
                {
                    return false;
                }
                else
                {
                    int DataCount = backList.Where(p => p.Address == Address).Count<tbBlackList>();
                    if(DataCount>0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               

        
          
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Info, string.Format("检索黑名单异常:{0}", ex.Message));
                return true;
            }
        }
    }
}
