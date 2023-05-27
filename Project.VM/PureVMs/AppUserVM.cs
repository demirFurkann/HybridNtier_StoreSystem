using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Project.VM.PureVMs
{
    public class AppUserVM
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; }
        public ProfileVM Profile { get; set; }


        public int? ProfileID { get; set; }
        


    }
}
