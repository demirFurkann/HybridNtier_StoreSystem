using Project.VM.PureVMs;

namespace Project.MVCUI.Data.PageVMs
{
    public class OrderDetailAddUpdatePageVM
    {
        public OrderDetailVM OrderDetail { get; set; }

        public List<ProductVM> Product { get; set; }

        public List<EmployeVM> Employe { get; set; }

        public List<AppUserVM>AppUser { get; set; }
    }
}
