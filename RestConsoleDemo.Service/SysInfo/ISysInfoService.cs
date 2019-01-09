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
    [ServiceContract(Namespace = "系统信息接口")]
    public interface  ISysInfoService
    {
        [OperationContract, WebInvoke(UriTemplate = "/GetAllLogList", Method = "POST",
  ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取文件列表")]
        string GetAllLogList(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/LogRead", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取文件列表")]
        string LogRead(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetAllLoginViewInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取登录记录")]
        string GetAllLoginViewInfo(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetLoginAllCount", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取登录记录总数")]
        string GetLoginAllCount(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllBlackListInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取黑名单记录")]
        string GetAllBlackListInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/AddBlackList", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("新增黑名单记录")]
        string AddBlackList(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/DeleteBlackList", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("删除黑名单记录")]
        string DeleteBlackList(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateBlackList", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改黑名单记录")]
        string UpdateBlackList(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetBlackListById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取黑名单记录ById")]
        string GetBlackListById(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllNoticeInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取通知信息")]
        string GetAllNoticeInfo(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetNoticeById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取通知ById")]
        string GetNoticeById(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetNoticeItemById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取通知明细ById")]
        string GetNoticeItemById(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/AddNotice", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("增加通知")]
        string AddNotice(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateNotice", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改通知")]
        string UpdateNotice(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateNoticeStatus", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改通知状态")]
        string UpdateNoticeStatus(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/DeleteNotice", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("删除通知")]
        string DeleteNotice(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetTreeParameter", Method = "POST",
  ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取参数树信息")]
        string GetTreeParameter(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetAllParameterInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取参数信息")]
        string GetAllParameterInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GetParameterById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取参数ById")]
        string GetParameterById(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/AddParameter", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("增加参数")]
        string AddParameter(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateParameter", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改参数")]
        string UpdateParameter(Stream stream);




        [OperationContract, WebInvoke(UriTemplate = "/DeleteParameter", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("删除参数")]
        string DeleteParameter(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GetParameterSelect", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("根据参数信息")]
        string GetParameterSelect(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetAllBillNoInfo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取单据编号记录")]
        string GetAllBillNoInfo(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/AddBillNo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("新增单据编号记录")]
        string AddBillNo(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/DeleteBillNo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("删除单据编号记录")]
        string DeleteBillNo(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/UpdateBillNo", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("修改单据编号记录")]
        string UpdateBillNo(Stream stream);



        [OperationContract, WebInvoke(UriTemplate = "/GetBillNoById", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取单据编号记录ById")]
        string GetBillNoById(Stream stream);


        [OperationContract, WebInvoke(UriTemplate = "/GeetestInit", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("极验证初始化")]
        string GeetestInit(Stream stream);

        [OperationContract, WebInvoke(UriTemplate = "/GeetestCheck", Method = "POST",
ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("极验证检查")]
        string GeetestCheck(Stream stream);

   
    }
}
