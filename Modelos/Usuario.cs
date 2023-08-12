using Microsoft.AspNetCore.Identity;

namespace locationapi.Modelos
{
    public class Usuario
    {
        public int ID { get; set; }
        public int ID_DelCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Contraseña { get; set; }
        public string CorreoElectrónico { get; set; }
        public string TeléfonoCelular { get; set; }
        public string TeléfonoAlterno { get; set; }
        public string Rol { get; set; }
    }
}
