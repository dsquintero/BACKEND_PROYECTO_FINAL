using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class UserRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string First_name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Last_name { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])([A-Za-z\d$@$!%*?&]|[^ ]){8,15}$", ErrorMessage = "La contraseña debe tener mínimo 8 caracteres y máximo 15 sin espacios, debe tener al menos 1 número, 1 minúscula, 1 mayúscula y 1 caracter especial")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Por favor digite una dirección de correo válida")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Username { get; set; }
    }
}
