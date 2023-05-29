using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int? AppUserID { get; set; }
        public int? EmployeeID { get; set; }

        public decimal TotalPrice { get; set; }
        public short Quantity { get; set; }

        //Relational Properties

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
