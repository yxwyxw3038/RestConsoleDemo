using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class FilterModel
    {
        public string column { get; set; }//过滤条件中使用的数据列
        public string action { get; set; }//过滤条件中的操作:==、!=等
        public string logic { get; set; }//过滤条件之间的逻辑关系：AND和OR
        public string value { get; set; }//过滤条件中的操作的值
        public string dataType { get; set; }//过滤条件中的操作的字段的类型
    }
}
