using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class EmployeManager:BaseManager<Employee>,IEmployeManager
    {
        IEmployeRepository _empRep;
        public EmployeManager(IEmployeRepository empRep):base(empRep)
        {
            _empRep = empRep;
        }
    }
}
