using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DAL.Repositories.Concretes;
using Project.ENTITIES.Models;
using Project.MVCUI.Data.PageVMs;
using Project.MVCUI.ShoppingTools;
using Project.VM.PureVMs;
using X.PagedList;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        IOrderDetailManager _ordDetMan;
        IProductManager _productMan;
        ICategoryManager _categoryMan;
        public ShoppingController(IOrderDetailManager ordDetMan, IProductManager productMan, ICategoryManager categoryMan, IHttpContextAccessor contx)
        {
            _ordDetMan = ordDetMan;
            _productMan = productMan;
            _categoryMan = categoryMan;
            _contx = contx;
        }

        public IActionResult ShoppingList(int? page, int? categoryID)
        {
            int pageSize = 4; // Her sayfada gösterilecek ürün sayısı

            IQueryable<ProductVM> query = categoryID == null ?
                _productMan.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new ProductVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    UnitsInStock = x.UnitsInStock,
                    SalePrice = x.SalePrice,
                    CategoryName = x.Category != null ? x.Category.CategoryName : "",
                    Status = x.Status.ToString(),
                }).AsQueryable() : // AsQueryable() ekledik
                _productMan.Where(x => x.CategoryID == categoryID && x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new ProductVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    UnitsInStock = x.UnitsInStock,
                    SalePrice = x.SalePrice,
                    CategoryID = x.CategoryID,
                    CategoryName = x.Category != null ? x.Category.CategoryName : "",
                    Status = x.Status.ToString(),
                }).AsQueryable(); // AsQueryable() ekledik

            IPagedList<ProductVM> products = query.ToPagedList(page ?? 1, pageSize);

            PaginationShoppingVM pavm = new PaginationShoppingVM
            {
                PagedProducts = products,
                Categories = _categoryMan.Select(x => new CategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                }).ToList()
            };

            if (categoryID != null)
            {
                TempData["catID"] = categoryID;
            }
            return View(pavm);
        }

        public IActionResult AddToCart(int id)
        {
            Cart cart = HttpContext.Session.GetObject<Cart>("scart");

            if (cart == null)
                cart = new Cart();
            Product add = _productMan.Find(id);

            CartItem ci = new CartItem
            {
                ID = add.ID,
                Name = add.ProductName,
                Amount = add.UnitsInStock,
                Price = add.SalePrice,
            };
            cart.SepeteEkle(ci);

            HttpContext.Session.SetObject("scart", cart);

            return RedirectToAction("ShoppingList");

        }

        public IActionResult CartPage()
        {
            Cart cart = HttpContext.Session.GetObject<Cart>("scart");

            if (cart != null)
            {
                CartPageVM cpvm = new CartPageVM
                {
                    Cart = cart
                };

                return View(cpvm);
            }

            ViewBag.SepetBos = "Sepetinizde Ürün Bulunamamaktadır";
            return RedirectToAction("ShoppingList");
        }






    }
}
