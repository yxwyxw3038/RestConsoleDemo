using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.Service
{
    [ServiceContract(Namespace = "流程接口")]
    public interface IFlowInfoService
    {
        [OperationContract, WebInvoke(UriTemplate = "/GetAllFlowInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取流程信息列表")]
        string GetAllFlowInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetFlowInfoByMenuId", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("根据菜单获取流程信息")]
        string GetFlowInfoByMenuId(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetFlowByCode", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("根据Code获取流程信息列表")]
        string GetFlowByCode(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/AddFlow", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("新增流程接口")]
        string AddFlow(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/UpdateFlow", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改流程接口")]
        string UpdateFlow(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/DeleteFlow", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("删除流程接口")]
        string DeleteFlow(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/UpdateFlowStatus", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("更新流程状态接口")]
        string UpdateFlowStatus(Stream stream);
    }
}
