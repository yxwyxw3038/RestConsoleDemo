using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public  class TreeModel
    {
        public string id { get; set; }

        public string label { get; set; }

        public List<TreeModel> children { get; set; }
    }
}
