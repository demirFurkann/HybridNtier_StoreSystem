using Project.VM.PureVMs;
using X.PagedList;

namespace Project.MVCUI.Data.PageVMs
{
    public class PaginationShoppingVM
    {
        public IPagedList<ProductVM> PagedProducts { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
