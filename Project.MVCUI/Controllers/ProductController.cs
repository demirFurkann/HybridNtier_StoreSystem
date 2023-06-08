using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;
using Project.MVCUI.Data.PageVMs;
using Project.VM.PureVMs;
using System.Drawing.Text;

namespace Project.MVCUI.Controllers
{
    public class ProductController : Controller
    {
        IProductManager _pMan;
        ICategoryManager _cMan;

        public ProductController(IProductManager pMan, ICategoryManager cMan)
        {
            _pMan = pMan;
            _cMan = cMan;
        }
        private List<ProductVM> GetPassives()
        {
            return _pMan.Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted).Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitsInStock = x.UnitsInStock,
                PurchasePrice = x.PurchasePrice,
                SalePrice = x.SalePrice,
                CategoryName = x.Category.CategoryName,
                Status = x.Status.ToString()
            }).ToList();
        }

        public IActionResult PassiveProducts()
        {
            List<ProductVM> products = GetPassives();
            ListProductPageVM pvm = new ListProductPageVM
            {
                Products = products,
            };
            return View(pvm);
        }

        private List<ProductVM> GetProductsVMs()
        {
            return _pMan.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitsInStock = x.UnitsInStock,
                PurchasePrice = x.PurchasePrice,
                SalePrice = x.SalePrice,

                CategoryName = x.Category.CategoryName,
                Status = x.Status.ToString(),
            }).ToList();
        }
        private List<CategoryVM> GetCategoryVMs()
        {
            return _cMan.Select(x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description,

            }).ToList();
        }


        public IActionResult ListProducts()
        {
            List<ProductVM> products = GetProductsVMs();
            ListProductPageVM lpvm = new ListProductPageVM
            {
                Products = products,
            };

            return View(lpvm);
        }

        public IActionResult AddProduct()
        {
            List<CategoryVM> categories = GetCategoryVMs();
            AddUpdateProductPageVM apvm = new AddUpdateProductPageVM
            {
                Categories = categories,
            };
            return View(apvm);
        }
        [HttpPost]
        public IActionResult AddProduct(ProductVM product)
        {
            Product p = new Product
            {
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                SalePrice = product.SalePrice,
                PurchasePrice = product.PurchasePrice,
                CategoryID = product.CategoryID,
            };
            _pMan.Add(p);
            return RedirectToAction("ListProducts");
        }

        public IActionResult UpdateProduct(int id)
        {
            List<CategoryVM> categories = GetCategoryVMs();
            AddUpdateProductPageVM apvm = new AddUpdateProductPageVM
            {
                Categories = categories,
                Product = _pMan.Where(x => x.ID == id).Select(x => new ProductVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    UnitsInStock = x.UnitsInStock,
                    SalePrice = x.SalePrice,
                    PurchasePrice = x.PurchasePrice,
                    CategoryID = x.CategoryID,
                }).FirstOrDefault()
            };
            return View(apvm);
        }
        [HttpPost]
        public IActionResult UpdateProduct(ProductVM product)
        {
            Product toBeUpdated = _pMan.Find(product.ID);
            toBeUpdated.ProductName = product.ProductName;
            toBeUpdated.PurchasePrice = product.PurchasePrice;
            toBeUpdated.SalePrice = product.SalePrice;
            toBeUpdated.UnitsInStock = product.UnitsInStock;
            toBeUpdated.CategoryID = product.CategoryID;
            _pMan.Update(toBeUpdated);
            return RedirectToAction("ListProducts");
        }

        public IActionResult DeleteProduct(int id)
        {
            _pMan.Delete(_pMan.Find(id));
            return RedirectToAction("ListProducts");
        }
    }
}
