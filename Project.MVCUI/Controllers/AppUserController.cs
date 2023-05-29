using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
 
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Data.PageVMs;
using Project.VM.PureVMs;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.MVCUI.Controllers
{
    public class AppUserController : Controller
    {
        IAppUserManager _aMan;
        public AppUserController(IAppUserManager aMan)
        {
            _aMan = aMan;
        }

        private List<AppUserVM> GetAppUserVMs()
        {
            return _aMan.Select(x => new AppUserVM
            {
                ID = x.ID,
                FirstName = x.Profile.FirstName,
                LastName = x.Profile.LastName,
                UserRole = x.Role.ToString(),
                Status = x.Status.ToString() // Enum türündeki Status'ı stringe dönüştürme
            })
            .AsEnumerable() // Verileri belleğe çekmek için AsEnumerable() kullanımı
            .Where(x => x.Status != DataStatus.Deleted.ToString()) // Bellekteki verilere göre karşılaştırma yapma
            .ToList();
        }


        public IActionResult ListAppUsers(int pageNumber = 1)
        {
            int pageSize = 2; // Her sayfada gösterilecek kullanıcı sayısı

            // Tüm kullanıcıları veritabanından alın
            List<AppUserVM> allAppUsers = GetAppUserVMs();

            // Sayfalama işlemi için kullanıcıları uygun sayfalara bölen kod
            IPagedList<AppUserVM> pagedAppUsers = allAppUsers.ToPagedList(pageNumber, pageSize);

            // PaginationVM oluşturarak sayfalama verilerini ViewModel'e ekleyin
            PaginationVM paginationVM = new PaginationVM
            {
                PagedAppUsers = pagedAppUsers,
                AppUser = new AppUserVM(), // Gerekli değilse null da atanabilir
                AppUsers = pagedAppUsers.ToList() // İsteğe bağlı olarak tüm kullanıcıları da ekleyebilirsiniz
            };

            return View(paginationVM);

        }

        public IActionResult AddAppUser()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddAppUser(AppUserVM appUser)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("AddAppUser");
            //}
            AppUser ap = new AppUser
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Profile = new AppUserProfile
                {
                    FirstName = appUser.Profile.FirstName,
                    LastName = appUser.Profile.LastName,
                }
            };
            _aMan.Add(ap);
            return RedirectToAction("ListAppUsers");
        }

        public IActionResult UpdateAppUser(int id)
        {
            AppUserVM avm = _aMan.Where(x => x.ID == id).Select(x => new AppUserVM
            {
                ID = x.ID,
                UserName = x.UserName,
                Password = x.Password,
                ProfileID = x.Profile.ID,
                Profile = new ProfileVM
                {
                    FirstName=x.Profile.FirstName,
                    LastName=x.Profile.LastName,
                }


            }).FirstOrDefault();

            AddUpdateAppUserPageVM apvm = new AddUpdateAppUserPageVM
            {
                AppUser = avm,


            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult UpdateAppUser(AppUserVM appUser)
        {
            AppUser toBeUpdated = _aMan.Find(appUser.ID);

            // Değişiklikleri uygulayın
            toBeUpdated.UserName = appUser.UserName;
            toBeUpdated.Password = appUser.Password;
            toBeUpdated.Profile.FirstName = appUser.Profile.FirstName;
            toBeUpdated.Profile.LastName = appUser.Profile.LastName;
            _aMan.Update(toBeUpdated);
            return RedirectToAction("ListAppUsers");
        }


        public IActionResult DeleteAppUser(int id)
        {
            _aMan.Delete(_aMan.Find(id));
            return RedirectToAction("ListAppUsers");
        }

    }

}



