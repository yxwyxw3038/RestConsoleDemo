using Newtonsoft.Json;
using RestConsoleDemo.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Helper
{
     public static  class ResponseHelper
    {
        public static string ResponseMsg(string Code,string Message,string Str)
        {
            if(string.IsNullOrEmpty(Code))
            {
                Code = "-1";
            }
            if (string.IsNullOrEmpty(Message))
            {
                Message = "";
            }
            if (string.IsNullOrEmpty(Str))
            {
                Str = "";
            }
            ResponseMessage msg = new ResponseMessage() { Code = Code, Message = Message, Data = Str };
            string  str = JsonConvert.SerializeObject(msg);
            return str;
        }
        public static string ResponseMsg(string Code, string Message, string Str,int Count)
        {
            if (string.IsNullOrEmpty(Code))
            {
                Code = "-1";
            }
            if (string.IsNullOrEmpty(Message))
            {
                Message = "";
            }
            if (string.IsNullOrEmpty(Str))
            {
                Str = "";
            }
            ResponseMessage msg = new ResponseMessage() { Code = Code, Message = Message, Data = Str, DataCount= Count.ToString() };
            string str = JsonConvert.SerializeObject(msg);
            return str;
        }
    }
}
