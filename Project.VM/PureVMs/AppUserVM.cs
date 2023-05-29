using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Project.VM.PureVMs
{
    public class AppUserVM
    {
        public int ID { get; set; }

        [StringLength(10,ErrorMessage ="Abartma")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen Ad Alanını Boş Geçmeyiniz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen Soyad Alanını Boş Geçmeyiniz")]
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; }
        public ProfileVM Profile { get; set; }


        public int? ProfileID { get; set; }
        


    }
}
