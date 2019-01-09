using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsoleDemo.BLL.Model
{
    public  class MenuTreeModel
    {
        public string Id { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string ParentId { get; set; }
        public string MenuName { get; set; }

        public List<MenuTreeModel> Node { get; set; }
    }
}
