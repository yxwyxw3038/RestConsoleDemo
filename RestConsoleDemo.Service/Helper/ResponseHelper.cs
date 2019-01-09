using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service.Helper
{
     public static   class ResponseHelper
    {
        public static void SetHeaderInfo()
        {
            //MessageHeader<string> mh = new MessageHeader<string>("abcde");
            //MessageHeader header = mh.GetUntypedHeader("Access-Control-Allow-Origin", "*");
            //OperationContext.Current.OutgoingMessageHeaders.Add(header);
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", WebOperationContext.Current.IncomingRequest.Headers["ORIGIN"]);
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Credentials", "true");
            // WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "*");
            // WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, DELETE");

                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");

            }
        }
    }
}
