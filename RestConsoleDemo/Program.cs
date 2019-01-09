using NLog;
using RestConsoleDemo.BLL.SysInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestConsoleDemo
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Console.Title = "ConsoleDemo";
            logger.Log(LogLevel.Info, string.Format("程序启动！"));
            LoadAssembly();
            RunSecurity();
            //RunMyWebStock();
            Console.Read();
        }
        public static void LoadAssembly()
        {

            int num = 0;
            //DLL所在的绝对路径 
            Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "RestConsoleDemo.Service.dll");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                string tempName = type.Name;
                if (tempName.IndexOf("Service") > 0 && tempName.Substring(0, 1) != "I")
                {
                    //为每个服务创建一个线程
                    Thread t = new Thread(new ParameterizedThreadStart(MyThread));
                    t.Start(tempName);
                    num++;
                }
            }

           
        }
        private static void MyThread(object name)
        {
            string tempName = name.ToString();
            try
            {
                Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "RestConsoleDemo.Service.dll");

              

                Type personType = assembly.GetType(string.Format("RestConsoleDemo.Service.{0}", tempName));

                ServiceHost host = new ServiceHost(personType);
                host.Opened += delegate
                {
                  
                    logger.Log(LogLevel.Info, string.Format("{0}服务已加载！", personType.ToString()));
                };

                host.Open();
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("启动{0}对应REST服务失败，原因：{1}...", tempName, ex.Message));
            }
        }

        public static void RunSecurity()
        {
            try
            {
                SecurityBill.LoadBlackList();
                Thread t = new Thread(new ThreadStart(Security));
                t.Start();

            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("启动安全心跳失败，原因：{0}...", ex.Message));
            }
        }
        private static void Security()
        {

            while (true)
            {

                logger.Log(LogLevel.Info, string.Format("开始启动安全心跳！"));
                SecurityBill.AnalysisBlackList();
                logger.Log(LogLevel.Info, string.Format("结束安全心跳！"));
                Thread.Sleep(60000);
            };

        }
        public static void RunMyWebStock()
        {
            try
            {

                Thread t = new Thread(new ThreadStart(MyWebStock));
                t.Start();

            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("启动MyWebStock失败，原因：{0}...", ex.Message));
            }
        }
        private static void MyWebStock()
        {

            string tempName = "MyWebStock";
            string MethodName = "RunService";
            //string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
            string StockPort = System.Configuration.ConfigurationManager.AppSettings["StockPort"];
            try
            {
                Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "RestConsoleDemo.Service.dll");
                Type personType = assembly.GetType(string.Format("RestConsoleDemo.Service.{0}", tempName));
                MethodInfo method = personType.GetMethod(MethodName);
                object obj = Activator.CreateInstance(personType);
                //object[] parameters = new object[] { BaseUrl, StockPort };
                object[] parameters = new object[] {  StockPort };
                method.Invoke(obj, parameters);
                //method.Invoke(obj, null);

            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("启动{0}服务失败，原因：{1}...", tempName, ex.Message));
            }

        }


    }
}
