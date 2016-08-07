using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    class ProductTree
    {
        public HierarchyId OrgNode { get; set; }
        public int OrgLevel { get; private set; }
        public string TreeName { get; set; }
        public int? ParentId { get; set; }
    }
}
