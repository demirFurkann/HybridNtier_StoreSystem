
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using X.PagedList;

namespace Project.MVCUI.Data.PageVMs
{
    public class PaginationVM
    {
        public IPagedList<AppUserVM> PagedAppUsers { get; set; }
        public List<AppUserVM> AppUsers { get; set; }
        public AppUserVM AppUser { get; set; }
    }
}
