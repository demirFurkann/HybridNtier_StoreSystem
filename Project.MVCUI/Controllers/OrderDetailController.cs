using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;
using Project.MVCUI.Data.PageVMs;
using Project.VM.PureVMs;

namespace Project.MVCUI.Controllers
{
    public class OrderDetailController : Controller
    {
        IOrderDetailManager _ordDetMan;
        IAppUserManager _appUsMan;
        IEmployeManager _employeMan;
        IProductManager _productMan;
        public OrderDetailController(IAppUserManager appUsMan, IEmployeManager employeMan, IOrderDetailManager ordDetMan, IProductManager productManager)
        {
            _appUsMan = appUsMan;
            _employeMan = employeMan;
            _ordDetMan = ordDetMan;
            _productMan = productManager;
        }

        private List<AppUserVM> GetAppUserVMs()
        {
            return _appUsMan.Select(x => new AppUserVM
            {
                ID = x.ID,
                UserName = x.UserName,

            }).ToList();
        }
        private List<EmployeVM> GetEmployeVMs()
        {
            return _employeMan.Select(x => new EmployeVM
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToList();
        }
        private List<OrderDetailVM> GetOrderDetailVMs()
        {
            return _ordDetMan.Select(x => new OrderDetailVM
            {
                ID = x.ID,
                SalesPrice = x.Product.SalePrice,
                EmployeName = x.Employee.FirstName,
                AppUserName = x.AppUser.Profile.FirstName,
                ProductName = x.Product.ProductName,

            }).ToList();
        }
        private List<ProductVM> GetProductVMs()
        {
            return _productMan.Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                SalePrice = x.SalePrice
            }).ToList();
        }

        public IActionResult ListOrderDetails(string search)
        {
            List<OrderDetailVM> orderDetails = GetOrderDetailVMs();
            if (orderDetails != null && !String.IsNullOrEmpty(search))
            {
                orderDetails = orderDetails.Where(x => x.AppUserName != null && x.AppUserName.ToLower().Contains(search.ToLower())).ToList();
            }

            OrderDetailListPageVM odlpvm = new OrderDetailListPageVM
            {
                OrderDetails = orderDetails,
            };
            return View(odlpvm);
        }
        public IActionResult AddOrderDetail()
        {
            List<EmployeVM> employes = GetEmployeVMs();
            List<AppUserVM> appUsers = GetAppUserVMs();
            List<ProductVM> products = GetProductVMs();
            OrderDetailAddUpdatePageVM order = new OrderDetailAddUpdatePageVM
            {
                AppUser = appUsers,
                Employe = employes,
                Product = products,
                OrderDetail = new OrderDetailVM()
            };

            return View(order);
        }
        [HttpPost]
        public IActionResult AddOrderDetail(OrderDetailVM orderDetail)
        {
            Employee employee = _employeMan.Find(orderDetail.EmployeeID);
            AppUser appUser = _appUsMan.Find(orderDetail.AppUserID);
            Product product = _productMan.Find(orderDetail.ProductID);

            OrderDetail ord = new OrderDetail
            {
                Employee = employee,
                Product = product,
                AppUser = appUser,
                EmployeeID = orderDetail.EmployeeID,
                ProductID = orderDetail.ProductID,
                AppUserID = orderDetail.AppUserID,
                TotalPrice = orderDetail.SalesPrice,
                Order = new Order()
            };
            _ordDetMan.Add(ord);

            return RedirectToAction("ListOrderDetails");
        }
    }
}
