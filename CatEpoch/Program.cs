using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess;
using CatEpoch.DataAccess.POCO;

namespace CatEpoch
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new DBC();
            var q = ctx.ProductGroups.SelectMany(p => ctx.Products, (group, product) => new
            {
                groupname = group.GroupName,
                productid = product.Id,
                productname = product.ProductName,
                groupid = group.Id
            }).Where(g => g.groupid > 1).Take(1000).OrderBy(g => g.groupid);


            foreach (var group in q)
            {
                Console.WriteLine("{0}\t{1} {2} {3}", group.groupid, group.groupname, group.productid, group.productname);
            }
            Console.ReadLine();
            

          //  List<string> monthNameslList = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToList();

          //  int x = 2;
          //  var monthList = monthNameslList.Select(
          //     m => new { Id = monthNameslList.IndexOf(m) + 1, Name = m }).Where(m => m.Id == x);
           
           

          //// var x = int.Parse(new DateTime(1, 1, 1).Month.ToString());

          //  foreach (var m in monthList)
          //  {
                
                
          //      Console.WriteLine("{0}",m.Name);    
          //  }
            
          //  Console.ReadLine();

        }
    }
}
