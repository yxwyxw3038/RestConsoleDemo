using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestConsoleDemo.BLL.Helper;
using RestConsoleDemo.BLL.Model;
using RestConsoleDemo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.SysInfo
{
    public class FlowBill
    {
        public static string GetAllFlowInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<v_FlowViewInfo> tblist = new List<v_FlowViewInfo>();


                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    DataCount = myDbContext.v_FlowViewInfo.Where(LinqHelper.GetFilterExpression<v_FlowViewInfo>(whereList).Compile()).Count<v_FlowViewInfo>();

                }
                else
                {
                    DataCount = myDbContext.v_FlowViewInfo.Count<v_FlowViewInfo>();
                }
                if (whereList.Count > 0)
                {
                    tblist = myDbContext.v_FlowViewInfo.Where(LinqHelper.GetFilterExpression<v_FlowViewInfo>(whereList).Compile()).OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (tblist == null)
                    {
                        tblist = new List<v_FlowViewInfo>();
                    }
                }
                else
                {
                    tblist = myDbContext.v_FlowViewInfo.OrderByDescending(p => p.CreateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无流程信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetFlowInfoByMenuId(int MenuId)
        {
            string str = string.Empty;


            try
            {
             
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<v_FlowViewInfo> tblist = new List<v_FlowViewInfo>();


                int DataCount = 0;

                tblist = myDbContext.v_FlowViewInfo.Where(p => p.MenuId == MenuId).ToList();
                if (tblist != null && tblist.Count > 0)
                {

                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(tblist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "无流程信息", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string GetFlowByCode(string Code)
        {
            string str = string.Empty;
            try
            {
                FlowModel temp = new FlowModel();
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                v_FlowViewInfo Flow = new v_FlowViewInfo();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                Flow = myDbContext.v_FlowViewInfo.Where(p => p.Code== Code).ToList().FirstOrDefault();
                if (Flow != null )
                {
                    List<tbFlowStep> FlowStep = new List<tbFlowStep>();
                    FlowStep= myDbContext.tbFlowStep.Where(p => p.FlowCode == Code).ToList();

                    List<v_FlowStepUserViewInfo> FlowStepUser = new List<v_FlowStepUserViewInfo>();
                    FlowStepUser = myDbContext.v_FlowStepUserViewInfo.Where(p => p.FlowCode == Code).ToList();

                    temp.Flow = Flow;
                    temp.FlowStep = FlowStep;
                    temp.FlowStepUser = FlowStepUser;
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "流程不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string AddFlow(string Flowstr)
        {
            string str = string.Empty;
            try
            {
                DateTime now = DateTime.Now;
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                FlowModel tb = JsonConvert.DeserializeObject<FlowModel>(Flowstr);
                tbFlow Flow = new tbFlow();
                ObjectHelper.CopyValue(tb.Flow, Flow);
                List<tbFlowStep> FlowStep = tb.FlowStep;
                List<v_FlowStepUserViewInfo> FlowStepUser = tb.FlowStepUser;
                if(tb==null)
                {
                    throw new Exception("流程数据异常！");
                }
                if(FlowStep==null|| FlowStep.Count<=0)
                {
                    throw new Exception("流程步骤数据异常！");
                }
                if (FlowStepUser == null || FlowStepUser.Count <= 0)
                {
                    throw new Exception("流程步骤数据异常！");
                }
                //string Code = Guid.NewGuid().ToString();
                string No = BillNoBill.GetBillNo(myDbContext, "LC");
               
                Flow.CreateTime = now;
                Flow.UpdateTime = now;
                Flow.status = 0;
                //Flow.Code = Code;
                Flow.No = No;
                myDbContext.tbFlow.Add(Flow);
                foreach (var st in FlowStep)
                {
                    st.CreateTime = now;
                    st.UpdateTime = now;
                    myDbContext.tbFlowStep.Add(st);
                }
                foreach (var st in FlowStepUser)
                {
                
                    st.CreateTime = now;
                    st.UpdateTime = now;
                    tbFlowStepUser temp = new tbFlowStepUser();
                    string[] keys = { "Id" };
                    ObjectHelper.CopyValue(st, temp);
                    myDbContext.tbFlowStepUser.Add(temp);
                }
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");


            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string UpdateFlow(string Flowstr)
        {
            string str = string.Empty;
            try
            {
                DateTime now = DateTime.Now;
                FlowModel tb = JsonConvert.DeserializeObject<FlowModel>(Flowstr);
                tbFlow Flow = new tbFlow();
                ObjectHelper.CopyValue(tb.Flow, Flow);
                List<tbFlowStep> FlowStep = tb.FlowStep;
                List<v_FlowStepUserViewInfo> FlowStepUser = tb.FlowStepUser;
                if (tb == null)
                {
                    throw new Exception("流程数据异常！");
                }
                if (FlowStep == null || FlowStep.Count <= 0)
                {
                    throw new Exception("流程步骤数据异常！");
                }
                if (FlowStepUser == null || FlowStepUser.Count <= 0)
                {
                    throw new Exception("流程步骤数据异常！");
                }
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbFlow newFlow = myDbContext.tbFlow.Where(p => p.Code == Flow.Code).ToList().FirstOrDefault();
                string[] keys = { "Code" };
                ObjectHelper.CopyValueNotKey(Flow, newFlow, keys);
                newFlow.UpdateTime = now;
                if (FlowStep != null && FlowStep.Count > 0)
                {
                    string [] FlowStepCode = new string[FlowStep.Count];
                    int count = 0;
                    foreach (var st in FlowStep)
                    {
                        FlowStepCode[count] = st.Code;
                        count++;
                        tbFlowStep temp = myDbContext.tbFlowStep.Where(p => p.Code==st.Code).ToList().FirstOrDefault();
                        if (temp != null)
                        {
                            ObjectHelper.CopyValueNotKey(st, temp, keys);
                            temp.UpdateTime = now;
                        }
                        else
                        {

                            st.FlowCode = Flow.Code;
                            st.UpdateTime = now;
                            myDbContext.tbFlowStep.Add(st);
                        }

                    }
                    List<tbFlowStep> tempList = myDbContext.tbFlowStep.Where(p => !(FlowStepCode).Contains(p.Code) && p.FlowCode == Flow.Code).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        foreach (var st in tempList)
                        {
                            myDbContext.tbFlowStep.Remove(st);
                        }
                    }


                }

                if (FlowStepUser != null && FlowStepUser.Count > 0)
                {
                    string[] FlowStepUserCode = new string[FlowStepUser.Count];
                    int count = 0;
                    foreach (var st in FlowStepUser)
                    {
                        FlowStepUserCode[count] = st.Code;
                        count++;
                        tbFlowStepUser temp = myDbContext.tbFlowStepUser.Where(p => p.Code == st.Code).ToList().FirstOrDefault();
                        if (temp != null)
                        {
                            ObjectHelper.CopyValue(st, temp);
                            temp.UpdateTime = now;
                        }
                        else
                        {

                            st.FlowCode = Flow.Code;
                            st.UpdateTime = now;
                            tbFlowStepUser newtemp = new tbFlowStepUser();
                            ObjectHelper.CopyValue(st, newtemp);
                            myDbContext.tbFlowStepUser.Add(newtemp);
                        }

                    }
                    List<tbFlowStepUser> tempList = myDbContext.tbFlowStepUser.Where(p => !(FlowStepUserCode).Contains(p.Code) && p.FlowCode == Flow.Code).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        foreach (var st in tempList)
                        {
                            myDbContext.tbFlowStepUser.Remove(st);
                        }
                    }


                }
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteFlow(string[] FlowList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int i = 0;
                foreach (string temp in FlowList)
                {

                    tbFlow deltbFlow = myDbContext.tbFlow.Where(p => p.Code == temp).FirstOrDefault();
                    if (deltbFlow == null)
                    {
                        continue;
                    }
                    if (deltbFlow.status !=0)
                    {
                        continue;
                    }
                    myDbContext.tbFlow.Remove(deltbFlow);
                    List<tbFlowStep> delFlowStep = myDbContext.tbFlowStep.Where(p => p.FlowCode == temp).ToList();
                    if (delFlowStep != null && delFlowStep.Count > 0)
                    {
                        foreach (var st in delFlowStep)
                        {
                            myDbContext.tbFlowStep.Remove(st);
                        }
                    }
                    List<tbFlowStepUser> delFlowStepUser = myDbContext.tbFlowStepUser.Where(p => p.FlowCode == temp).ToList();
                    if (delFlowStepUser != null && delFlowStepUser.Count > 0)
                    {
                        foreach (var st in delFlowStepUser)
                        {
                            myDbContext.tbFlowStepUser.Remove(st);
                        }
                    }

                }



                myDbContext.SaveChanges();
                string msg = string.Format("提交{0}条数据，成功删除{1}条", FlowList.Length, i);
                str = ResponseHelper.ResponseMsg("1", msg, "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }
    }
}
