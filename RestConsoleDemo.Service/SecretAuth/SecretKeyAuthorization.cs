using NLog;
using RestConsoleDemo.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service.SecretAuth
{
    /// <summary>
    ///     Rest安全认证
    /// </summary>
    public class SecretKeyAuthorization : ServiceAuthorizationManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        ///     基于默认的策略评估，检查给定操作上下文和可选消息的授权。
        /// </summary>
        /// <param name="operationContext">当前授权请求的 System.ServiceModel.OperationContext。</param>
        /// <returns>如果授予访问权限，则为 true；否则为 false。 默认值为 true。</returns>
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            MessageProperties properties = operationContext.IncomingMessageProperties;
            var url = properties.Via.PathAndQuery;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            logger.Log(LogLevel.Info, string.Format("用户：{0}:{1}访问服务,请求参数({2})！", endpoint.Address, endpoint.Port, url));

            //MessageHeader<string> mh = new MessageHeader<string>("abcde");
            //MessageHeader header = mh.GetUntypedHeader("Access-Control-Allow-Origin","*");
            //OperationContext.Current.OutgoingMessageHeaders.Add(header);
     

            return SecurityHelper.Analysis(endpoint.Address);
        }
    }

}
