using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.ViewModels
{
    public class SubssModel
    {
        public int id { get; set; }
        public string Name { get; set; }

    }
    public class MenusModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubssModel> Subs { get; set; }

    }
}
