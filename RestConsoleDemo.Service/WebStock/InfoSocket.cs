using Newtonsoft.Json;
using RestConsoleDemo.Service.Helper;
using RestConsoleDemo.Service.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RestConsoleDemo.Service.WebStock
{
    public class InfoSocket : WebSocketBehavior
    {
        private Dictionary<string, string> nameList = new Dictionary<string, string>();
        protected override async Task OnMessage(MessageEventArgs e)  
        {
            ResponseHelper.SetHeaderInfo();
            StreamReader reader = new StreamReader(e.Data);
            string text = reader.ReadToEnd();
            await Send("Message");

            //await Send(text);
        }
        protected override async Task OnClose(CloseEventArgs e)
        {
            Console.WriteLine("连接关闭" + Id);
            nameList.Remove(Id);
        }

        protected override async Task OnError(WebSocketSharp.ErrorEventArgs e)
        {
            var el = e;
        }

        protected override async Task OnOpen()
        {
            ResponseHelper.SetHeaderInfo();
            Console.WriteLine("建立连接" + Id);
            nameList.Add(Id, "游客" + Sessions.Count);
            await Send("open");

        }

    }
}
