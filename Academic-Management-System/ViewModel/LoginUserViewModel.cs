using System.ComponentModel.DataAnnotations;

namespace Task02_MVC.ViewModel
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The UserName is Required")]
        public string UserName { get; set; }
         
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me!!")]
        public bool RememberMe { get; set; }
    }
    
}
