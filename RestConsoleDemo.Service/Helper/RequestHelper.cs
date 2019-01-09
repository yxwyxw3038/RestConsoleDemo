using RestConsoleDemo.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service.Helper
{
     public static class RequestHelper
    {
        public static IPInfoModel RequestInfo()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string Address = endpoint.Address;
            string Port = endpoint.Port.ToString();
            IPInfoModel temp = new IPInfoModel()
            {
                Address = Address,
                Port = Port
            };
            return temp;
        }
    }
}
