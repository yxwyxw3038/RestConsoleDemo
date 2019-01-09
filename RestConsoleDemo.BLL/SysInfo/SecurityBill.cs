using NLog;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
    public static class SecurityBill
    {
        public static void LoadBlackList()
        {

            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbBlackList> templist = new List<tbBlackList>();
                int DataCount = 0;
                DataCount = myDbContext.tbBlackList.Count<tbBlackList>();
                logger.Log(LogLevel.Info, string.Format("已提取黑名单用户共计{0}条", DataCount));
                templist = myDbContext.tbBlackList.ToList();
                DataCacheHelper.Updata<tbBlackList>(templist);
                logger.Log(LogLevel.Info, string.Format("黑名单用户已加载至缓存"));



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AnalysisBlackList()
        {
            try
            {
               
                Logger logger = LogManager.GetCurrentClassLogger();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbBlackList> templist = new List<tbBlackList>();
                logger.Log(LogLevel.Info, string.Format("启动分析黑名单进程！"));
                string BlackCount = System.Configuration.ConfigurationManager.AppSettings["BlackCount"];
                int blackCount = string.IsNullOrEmpty(BlackCount) ? 0 : Convert.ToInt32(BlackCount);
                List<IPCountInfoModel> ipList= DataCacheHelper.GetData<IPCountInfoModel>();
                if(ipList!=null&& ipList.Count>0)
                {
                    ipList = ipList.Where(p => p.Count > blackCount).ToList();
                    if (ipList != null && ipList.Count > 0)
                    {
                        foreach (var temp in ipList)
                        {
                            int DataCount= myDbContext.tbBlackList.Where(p => p.Address == temp.Address && p.IsAble==1).Count<tbBlackList>();
                            if(DataCount>0)
                            {
                               
                            }
                            else
                            {
                                 DataCount = myDbContext.tbBlackList.Where(p => p.Address == temp.Address).Count<tbBlackList>();
                                if (DataCount > 0)
                                {
                                    tbBlackList tp1 = new tbBlackList()
                                    {
                                        Address = temp.Address,
                                      
                                    };
                                
                                    myDbContext.tbBlackList.Remove(tp1);
                                }

                                tbBlackList tp = new tbBlackList()
                                    {
                                        Address = temp.Address,
                                        IsAble = 1,
                                        CreateTime = DateTime.Now,
                                        Notes = string.Format("心跳周期内访问{0}次,系统自动屏蔽！", temp.Count)
                                    };
                                    myDbContext.tbBlackList.Add(tp);
                                
                                
                               

                            }
                         }
                        myDbContext.SaveChanges();
                     
                        logger.Log(LogLevel.Info, string.Format("通过本次分析，黑名单共新增{0}条记录!",ipList.Count));

                    }
                }
                ipList = new List<IPCountInfoModel>();
                DataCacheHelper.Updata<IPCountInfoModel>(ipList);
                logger.Log(LogLevel.Info, string.Format("结束分析黑名单进程！"));
                //缓存重新加载黑名单信息
                LoadBlackList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       





        }
}
