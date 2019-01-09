using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestConsoleDemo.BLL.SysInfo
{
    public static class LoginBill
    {

        public static string GetAllLoginViewInfo(string ParameterStr, int PageSize, int CurrentPage)
        {
            string str = string.Empty;


            try
            {
                List<FilterModel> whereList = new List<FilterModel>();
                if (!string.IsNullOrEmpty(ParameterStr))
                {
                    whereList = JsonConvert.DeserializeObject<List<FilterModel>>(ParameterStr);
                }
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<v_LoginViewInfo> returnlist = new List<v_LoginViewInfo>();
                List<v_LoginViewInfo> templist = new List<v_LoginViewInfo>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.v_LoginViewInfo.Where(LinqHelper.GetFilterExpression<v_LoginViewInfo>(whereList).Compile()).Count<v_LoginViewInfo>();

                }
                else
                {
                    DataCount = myDbContext.v_LoginViewInfo.Count<v_LoginViewInfo>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.v_LoginViewInfo.Where(LinqHelper.GetFilterExpression<v_LoginViewInfo>(whereList).Compile()).OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<v_LoginViewInfo>();
                    }
                }
                else
                {
                    templist = myDbContext.v_LoginViewInfo.OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }




                str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
            
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        //public static string GetAllLoginViewInfo(string ParameterStr, int PageSize, int CurrentPage)
        //{
        //    string str = string.Empty;


        //    try
        //    {
        //        List<FilterModel> whereList = new List<FilterModel>();
        //        if (!string.IsNullOrEmpty(ParameterStr))
        //        {
        //            whereList = JsonConvert.DeserializeObject<List<FilterModel>>(ParameterStr);
        //        }
        //        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
        //        timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        //        AchieveDBEntities myDbContext = new AchieveDBEntities();
        //        List<LoginViewModel> returnlist = new List<LoginViewModel>();
        //        List<tbUserToken> templist = new List<tbUserToken>();

        //        int DataCount = 0;
        //        if (whereList.Count > 0)
        //        {
        //            DataCount = myDbContext.tbUserToken.Where(LinqHelper.GetFilterExpression<tbUserToken>(whereList).Compile()).Count<tbUserToken>();

        //        }
        //        else
        //        {
        //            DataCount = myDbContext.tbUserToken.Count<tbUserToken>();
        //        }
        //        if (whereList.Count > 0)
        //        {
        //            templist = myDbContext.tbUserToken.Where(LinqHelper.GetFilterExpression<tbUserToken>(whereList).Compile()).OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        //            if (templist == null)
        //            {
        //                templist = new List<tbUserToken>();
        //            }
        //        }
        //        else
        //        {
        //            templist = myDbContext.tbUserToken.OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        //        }


        //        if (templist != null && templist.Count > 0)
        //        {
        //            foreach (var st in templist)
        //            {
        //                var query = from ut in myDbContext.tbUserToken
        //                            join u in myDbContext.tbUser on ut.UserId equals u.ID
        //                            where ut.Token == st.Token
        //                            select u;
        //                if (query != null)
        //                {
        //                    string AccountName = query.ToList()[0].AccountName;
        //                    string RealName = query.ToList()[0].RealName;

        //                    LoginViewModel temp = new LoginViewModel()
        //                    {
        //                        AccountName = AccountName,
        //                        RealName = RealName,
        //                        Address = st.Address,
        //                        CreateTime = st.CreateTime,
        //                        IsLoginOut = st.IsLoginOut,
        //                        LoginOutTime = st.LoginOutTime,
        //                        Port = st.Port,
        //                        Token = st.Token,
        //                        UpdateTime = st.UpdateTime,
        //                        UserId = st.UserId

        //                    };
        //                    returnlist.Add(temp);

        //                }
        //            }


        //            str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
        //            str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
        //        }
        //        else
        //        {
        //            str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
        //            str = ResponseHelper.ResponseMsg("-1", "登录信息不存在", str, DataCount);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
        //    }

        //    return str;
        //}

        public static string GetLoginAllCount()
        {

            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                int Count = myDbContext.tbUserToken.Count<tbUserToken>();





                str1 = ResponseHelper.ResponseMsg("1", "取数成功", Count.ToString());

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }
    }
}
