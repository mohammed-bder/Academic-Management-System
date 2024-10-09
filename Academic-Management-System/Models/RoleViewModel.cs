using System.ComponentModel.DataAnnotations;

namespace Task02_MVC.Models
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
