using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public class CascaderModel
    {
        public string value { get; set; }

        public string label { get; set; }

        public List<CascaderModel> children { get; set; }

    }
}
