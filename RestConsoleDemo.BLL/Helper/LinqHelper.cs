using RestConsoleDemo.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Helper
{
    public static class LinqHelper
    {
        public static Expression<Func<T, bool>> GetFilterExpression<T>(List<FilterModel> filterConditionList)
        {
            Expression<Func<T, bool>> condition = null;
            try
            {
                if (filterConditionList != null && filterConditionList.Count > 0)
                {
                    foreach (FilterModel filterCondition in filterConditionList)
                    {
                        Expression<Func<T, bool>> tempCondition = CreateLambda<T>(filterCondition);
                        if (condition == null)
                        {
                            condition = tempCondition;
                        }
                        else
                        {
                            if ("AND".Equals(filterCondition.logic))
                            {
                                condition = condition.And(tempCondition);
                            }
                            else
                            {
                                condition = condition.Or(tempCondition);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return condition;
        }


        public static Expression<Func<T, bool>> CreateLambda<T>(FilterModel filterCondition)
        {
            var parameter = Expression.Parameter(typeof(T), "p");//创建参数i

            MemberExpression member = Expression.PropertyOrField(parameter, filterCondition.column);
            Type t = member.Type;

            var constant = Expression.Constant(filterCondition.value);//创建常数

            //if(t.Name== "DateTime")
            //{
            //    object obj = filterCondition.value;
            //    DateTime time = Convert.ToDateTime(obj.ToString());
            //    constant = Expression.Constant(time, t);
            //}
            //if (t.Name == "Boolean")
            //{
            //    object obj = filterCondition.value;
            //    bool bo = bool.Parse(obj.ToString());
            //    constant = Expression.Constant(bo, t);
            //}
            object obj = filterCondition.value;
            switch (t.Name)
            {
                case "DateTime":

                    DateTime time = Convert.ToDateTime(obj.ToString());
                    constant = Expression.Constant(time, t);

                    break;
                case "Boolean":

                    bool bo = bool.Parse(obj.ToString());
                    constant = Expression.Constant(bo, t);
                    break;


                case "Int16":

                    constant = Expression.Constant(Convert.ToInt16(obj), t);
                    break;
                case "Int32":

                    constant = Expression.Constant(Convert.ToInt32(obj), t);
                    break;
                case "Int64":

                    constant = Expression.Constant(Convert.ToInt64(obj), t);
                    break;
                case "Decimal":
                    constant = Expression.Constant(Convert.ToDecimal(obj), t);
                    break;
                case "Double":
                    constant = Expression.Constant(Convert.ToDouble(obj), t);
                    break;
                case "Nullable`1":
                    if (t.FullName.Contains("Boolean"))
                    {
                        constant = Expression.Constant(Convert.ToBoolean(obj), t);
                    }
                    else if(t.FullName.Contains("Int32"))
                    {
                        constant = Expression.Constant(Convert.ToInt32(obj), t);
                    }
                    else if (t.FullName.Contains("Int16"))
                    {
                        constant = Expression.Constant(Convert.ToInt16(obj), t);
                    }
                    else if (t.FullName.Contains("Int64"))
                    {
                        constant = Expression.Constant(Convert.ToInt64(obj), t);
                    }
                    else if (t.FullName.Contains("Decimal"))
                    {
                        constant = Expression.Constant(Convert.ToDecimal(obj), t);
                    }
                    else if (t.FullName.Contains("Double"))
                    {
                        constant = Expression.Constant(Convert.ToDouble(obj), t);
                    }
                    else if (t.FullName.Contains("DateTime"))
                    {
                        DateTime time1 = Convert.ToDateTime(obj.ToString());
                        if ("<=".Equals(filterCondition.action))
                         {
                           
                            constant = Expression.Constant(time1.AddDays(1), t);
                        }
                        else
                        {
                            constant = Expression.Constant(time1, t);
                        }
                    }
                    break;
                default:

                    break;

            }
            if ("=".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
            }
            else if ("!=".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
            }
            else if (">".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
            }
            else if ("<".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
            }
            else if (">=".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
            }
            else if ("<=".Equals(filterCondition.action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
            }
            else if ("in".Equals(filterCondition.action))
            {
                return GetExpressionWithMethod<T>("Contains", filterCondition);
            }
            else if ("out".Equals(filterCondition.action))
            {
                return GetExpressionWithoutMethod<T>("Contains", filterCondition);
            }
            else
            {
                return null;
            }
        }


        public static Expression<Func<T, bool>> GetExpressionWithMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.column, filterCondition.value, parameterExpression);
            return Expression.Lambda<Func<T, bool>>(methodExpression, parameterExpression);
        }

        public static Expression<Func<T, bool>> GetExpressionWithoutMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.column, filterCondition.value, parameterExpression);
            var notMethodExpression = Expression.Not(methodExpression);
            return Expression.Lambda<Func<T, bool>>(notMethodExpression, parameterExpression);
        }

        /// <summary>
        /// 生成类似于p=>p.values.Contains("xxx");的lambda表达式
        /// parameterExpression标识p，propertyName表示values，propertyValue表示"xxx",methodName表示Contains
        /// 仅处理p的属性类型为string这种情况
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        private static MethodCallExpression GetMethodExpression(string methodName, string propertyName, string propertyValue, ParameterExpression parameterExpression)
        {
            var propertyExpression = Expression.Property(parameterExpression, propertyName);
            MethodInfo method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            return Expression.Call(propertyExpression, method, someValue);
        }



    }
}
