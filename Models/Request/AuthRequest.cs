using System.ComponentModel.DataAnnotations;

namespace locationapi.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string NombreDeUsuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
