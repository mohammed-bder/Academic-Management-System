using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task02_MVC.ViewModel
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string? Address { get; set; }
    }
}
