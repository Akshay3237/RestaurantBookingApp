using System.ComponentModel.DataAnnotations;

namespace Restorent_app.ViewModel
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
