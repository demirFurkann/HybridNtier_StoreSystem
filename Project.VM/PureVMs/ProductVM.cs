﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Status { get; set; }
      
        public string CategoryName { get; set; }
        public int? CategoryID { get; set; }


    }
}
