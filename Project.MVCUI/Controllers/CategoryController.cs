using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;
using Project.MVCUI.Data.PageVMs;
using Project.VM.PureVMs;

namespace Project.MVCUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryManager _catMan;
        public CategoryController(ICategoryManager catMan)
        {
            _catMan = catMan;
        }

        private List<CategoryVM> GetCategoriesVM()
        {
            return _catMan.Select(x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Status = x.Status.ToString(),
            }).ToList();
        }
        public IActionResult ListCategories()
        {
            List<CategoryVM> categories = GetCategoriesVM();
            ListCategoryPageVM lpvm = new ListCategoryPageVM
            {
                Categories = categories,
            };

            return View(lpvm);
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryVM category)
        {
            Category c = new Category
            {
                ID = category.ID,
                CategoryName = category.CategoryName,
                Description = category.Description,

            };
            _catMan.Add(c);
            return RedirectToAction("ListCategories");
        }
        public IActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = _catMan.Where(x => x.ID == id).Select(x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description,
            }).FirstOrDefault();

            AddUpdateCategoryPageVM apvm = new AddUpdateCategoryPageVM
            {
                Category = cvm
            };

            return View(apvm);
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryVM category)
        {
            Category updated = _catMan.Find(category.ID);
            updated.CategoryName = category.CategoryName;
            updated.Description = category.Description;
            _catMan.Update(updated);
            return RedirectToAction("ListCategories");
        }
        public IActionResult DeleteCategory(int id)
        {
            _catMan.Delete(_catMan.Find(id));
            return RedirectToAction("ListCategories");
        }
    }
}
