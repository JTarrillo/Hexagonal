using System.ComponentModel.DataAnnotations;

namespace Sicv1.Presentation.Models
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "El campo usuario es obligatorio")]
        //[Display(Name = "Usuario")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "El campo password es obligatorio")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Contraseña")]
        public string Password { get; set; }

        //public string HDUser { get; set; }
        //public string HDpass { get; set; }

        //public LoginViewModel() {
        //    Username = "11112222";
        //}
    }
}