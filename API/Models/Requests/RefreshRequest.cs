using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class RefreshRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string RefreshToken { get; set; }
    }
}
