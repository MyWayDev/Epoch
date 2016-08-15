using System;
using System.Collections.Generic;
using System.Data.Entity.Hierarchy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
    public class ProductTree
    {
        public int GroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
       
        public HierarchyId OrgNode { get; set; }
        public int OrgLevel { get; private set; }
        public string TreeName { get; set; }
        public int? ParentId { get; set; }
    }
}
