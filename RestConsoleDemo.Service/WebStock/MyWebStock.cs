using NLog;
using RestConsoleDemo.Service.WebStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace RestConsoleDemo.Service
{
    public class MyWebStock
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void RunService(string port)
        {
         
            try
            {
                 //var listener = new HttpListener();
                 //listener.Prefixes.Add(stockUrl);
                 //listener.Start();
                 //logger.Log(NLog.LogLevel.Info, string.Format("WebStock服务启动成功,服务地址({0})！", stockUrl));
                var wssv = new WebSocketServer(null, Convert.ToInt32(port));
                wssv.AddWebSocketService<InfoSocket>("/InfoSocket");
                //wssv.AddWebSocketService<InfoSocket>("");
            

                wssv.Start();
                logger.Log(NLog.LogLevel.Info, string.Format("WebStock服务启动成功,端口({0})！", port));

            }
            catch (Exception ex)
            {
                logger.Log(NLog.LogLevel.Info, string.Format("WebStock服务启动失败,端口({0})！原因:{1}", port, ex.Message));
            }

        }
    }
}
