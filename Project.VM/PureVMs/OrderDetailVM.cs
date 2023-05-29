using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class OrderDetailVM
    {
        public int ID { get; set; }

        public int AppUserID { get; set; }
        public string AppUserName { get; set; }

        public int EmployeeID { get; set; }
        public string EmployeName { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal SalesPrice { get; set; }



    }
}
