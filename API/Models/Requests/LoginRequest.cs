using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string username { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
    }
}
