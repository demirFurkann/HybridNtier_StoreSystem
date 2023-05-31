using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;

namespace Project.MVCUI.Controllers
{
    public class LoginController : Controller
    {
        IAppUserManager _appMan;
        IProfileManager _profileMan;
        public LoginController(IAppUserManager appMan, IProfileManager profileMan)
        {
            _appMan = appMan;
            _profileMan = profileMan;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AppUserVM appUser, ProfileVM profile)
        {
            if (_appMan.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı İsmi Mevcuttur";
                return View();
            }

            AppUser domainUser = new AppUser
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
            };
            _appMan.Add(domainUser);

            //if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
            //{
            //    AppUserProfile domainProfile = new AppUserProfile
            //    {
            //        ID = domainUser.ID,
            //        FirstName = profile.FirstName,
            //        LastName = profile.LastName,
            //    };
            //}

            return RedirectToAction("RegisterNow");
        }
        public IActionResult LoginOk()
        {
            return View();
        }

        public IActionResult RegisterNow(AppUserVM appUser, ProfileVM profile)
        {
            if (_appMan.FirstOrdefault(x => x.UserName == appUser.UserName && x.Password == appUser.Password) != null)
            {
                return View("LoginOk");
            }
            return View();
        }
    }
}
