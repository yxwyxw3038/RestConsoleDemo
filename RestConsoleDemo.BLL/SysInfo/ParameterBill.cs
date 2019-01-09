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
    public  class ParameterBill
    {
        public static string GetTreeParameter()
        {
            string str1 = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                AchieveDBEntities myDbContext = new AchieveDBEntities();


                var query = from m in myDbContext.tbParameter
                            where string.IsNullOrEmpty(m.FatherCode)
                            orderby m.UpdateTime descending
                            select m;
                List<string> listtemp = new List<string>();
                string temp = string.Empty;
                if (query != null)
                {
                    foreach (var tp in query)
                    {
                        string str = GetTreeParameter(tp.Code);
                        if (string.IsNullOrEmpty(str))
                        {


                            str = "{\"id\":\"" + tp.Code.ToString() + "\" , \"label\":\"" + tp.CodeName + "\"}";

                        }
                        else
                        {
                            str = "{\"id\":\"" + tp.Code.ToString() + "\" ,\"label\":\"" + tp.CodeName + "\" , \"children\":[" + str + "]  }";
                        }

                        listtemp.Add(str);




                    }

                    for (int i = 0; i < listtemp.Count; i++)
                    {
                        if (i != listtemp.Count - 1)
                        {
                            temp = string.Concat(temp, listtemp[i], ",");
                        }
                        else
                        {
                            temp = string.Concat(temp, listtemp[i]);
                        }
                    }
                    // temp = string.Format("{0}", temp);

                }
                else
                {
                    temp = "";
                }


                temp = "[" + temp + "]";


                str1 = ResponseHelper.ResponseMsg("1", "取数成功", temp);

            }
            catch (Exception ex)
            {
                str1 = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str1;
        }

        public static string GetTreeParameter(string Code)
        {

            AchieveDBEntities myDbContext = new AchieveDBEntities();
            List<string> listtemp = new List<string>();
            var query = from m in myDbContext.tbParameter
                        where m.FatherCode == Code
                        orderby m.UpdateTime descending
                        select m;

            if (query != null)
            {
                foreach (var tp in query)
                {
                    string str = GetTreeParameter(tp.Code);
                    if (string.IsNullOrEmpty(str))
                    {


                        str = "{\"id\":\"" + tp.Code.ToString() + "\" , \"label\":\"" + tp.CodeName + "\"}";

                    }
                    else
                    {
                        str = "{\"id\":\"" + tp.Code.ToString() + "\" , \"label\":\"" + tp.CodeName + "\", \"children\":[" + str + "]  }";
                    }

                    listtemp.Add(str);




                }
                string temp = string.Empty;
                for (int i = 0; i < listtemp.Count; i++)
                {
                    if (i != listtemp.Count - 1)
                    {
                        temp = string.Concat(temp, listtemp[i], ",");
                    }
                    else
                    {
                        temp = string.Concat(temp, listtemp[i]);
                    }
                }
                return temp;
            }
            else
            {
                return "";
            }



        }

        public static string GetAllParameterInfo(string ParameterStr, int PageSize, int CurrentPage)
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
                List<tbParameter> templist = new List<tbParameter>();

                int DataCount = 0;
                if (whereList.Count > 0)
                {
                    var  wherevar= LinqHelper.GetFilterExpression<tbParameter>(whereList).Compile();
                    DataCount = myDbContext.tbParameter.Where(wherevar).Count<tbParameter>();

                }
                else
                {
                    DataCount = myDbContext.tbParameter.Count<tbParameter>();
                }
                if (whereList.Count > 0)
                {
                    templist = myDbContext.tbParameter.Where(LinqHelper.GetFilterExpression<tbParameter>(whereList).Compile()).OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    if (templist == null)
                    {
                        templist = new List<tbParameter>();
                    }
                }
                else
                {
                    templist = myDbContext.tbParameter.OrderByDescending(p => p.UpdateTime).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                }


                if (templist != null && templist.Count > 0)
                {


                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str, DataCount);
                }
                else
                {
                    str = JsonConvert.SerializeObject(templist, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "信息不存在", str, DataCount);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string AddParameter(string Parameterstr)
        {
            string str = string.Empty;
            try
            {
                tbParameter tb = JsonConvert.DeserializeObject<tbParameter>(Parameterstr);
                tb.CreateTime = DateTime.Now;
                tb.UpdateTime = DateTime.Now;
                 string[] keys = { "id" };
                tbParameter newtb = new tbParameter();
                ObjectHelper.CopyValueNotKey(tb, newtb, keys);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                int DataCount = myDbContext.tbParameter.Where(p => p.Code == tb.Code).Count<tbParameter>();
                if (DataCount > 0)
                {
                    throw new Exception(string.Format("Code：{0}重复，请重新输入", tb.Code));
                }
                myDbContext.tbParameter.Add(newtb);
                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "保存成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }

        public static string DeleteParameter(string[] ParameterList)
        {
            string str = string.Empty;
            try
            {

                AchieveDBEntities myDbContext = new AchieveDBEntities();
                foreach (string temp in ParameterList)
                {
                    if (string.IsNullOrEmpty(temp))
                    {
                        continue;
                    }
                    int id = Convert.ToInt32(temp);
                    List<tbParameter> delList = myDbContext.tbParameter.Where(p => p.id == id).ToList();
                    if (delList != null && delList.Count > 0)
                    {
                        myDbContext.tbParameter.Remove(delList[0]);
                        string code = delList[0].Code;
                        List<tbParameter> delNextList = myDbContext.tbParameter.Where(p => p.FatherCode == code).ToList();
                        if (delNextList != null && delNextList.Count > 0)
                        {
                            foreach (var tempNext in delNextList)
                            {
                                myDbContext.tbParameter.Remove(tempNext);
                            }
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

        public static string UpdateParameter(string Parameterstr)
        {
            string str = string.Empty;
            try
            {
                tbParameter tb = JsonConvert.DeserializeObject<tbParameter>(Parameterstr);
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                tbParameter data = myDbContext.tbParameter.Where(p => p.id == tb.id).FirstOrDefault();
                if (data != null)
                {
                    int DataCount = myDbContext.tbParameter.Where(p => p.Code == tb.Code &&p.id!=tb.id ).Count<tbParameter>();
                    if (DataCount > 0)
                    {
                        throw new Exception(string.Format("Code：{0}重复，请重新输入", data.Code));
                    }
                    string[] keys = { "id" };
                    ObjectHelper.CopyValueNotKey(tb, data, keys);
                }
                else
                {
                    throw new Exception(string.Format("Key：{0}找不到相关数据", tb.id));
                }
              

                myDbContext.SaveChanges();
                str = ResponseHelper.ResponseMsg("1", "更新成功", "");

            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


        public static string GetParameterById(int Id)
        {
            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                tbParameter temp = new tbParameter();
                AchieveDBEntities myDbContext = new AchieveDBEntities();
                List<tbParameter> templist = myDbContext.tbParameter.Where(p => p.id == Id).ToList();
                if (templist != null && templist.Count > 0)
                {

                    temp = templist[0];
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                }
                else
                {
                    str = JsonConvert.SerializeObject(temp, Formatting.Indented, timeFormat);
                    str = ResponseHelper.ResponseMsg("-1", "数据不存在", str);
                }
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;
        }


        public static string GetParameterSelect(string Code)
        {

            string str = string.Empty;
            try
            {

                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                List<CascaderNullModel> returnlist = new List<CascaderNullModel>();
                AchieveDBEntities myDbContext = new AchieveDBEntities();

                List<tbParameter> templist = myDbContext.tbParameter.Where(p => p.FatherCode == Code).ToList();
                 if (templist != null && templist.Count > 0)
                    {
                        foreach(var st in templist )
                        {
                            CascaderNullModel t = new CascaderNullModel()
                            {
                                label = st.CodeName,
                                value = st.Code

                            };
                            returnlist.Add(t);
                        }
                        
                        str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("1", "取数成功", str);
                    }
                    else
                    {
                        str = JsonConvert.SerializeObject(returnlist, Formatting.Indented, timeFormat);
                        str = ResponseHelper.ResponseMsg("-1", "数据不存在", str);
                    }
              
            }
            catch (Exception ex)
            {
                str = ResponseHelper.ResponseMsg("-1", ex.Message, "");
            }

            return str;


        }



    }
}
