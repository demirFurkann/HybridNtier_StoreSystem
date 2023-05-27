using Project.VM.PureVMs;

namespace Project.MVCUI.Data.PageVMs
{
    public class AddUpdateProductPageVM
    {
        public ProductVM Product { get; set; }
        public List<CategoryVM> Categories{ get; set; }
    }
}
