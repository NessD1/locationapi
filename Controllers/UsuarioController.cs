using locationapi.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace locationapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost(Name = "GetUsuario")]
        //[Route("{id}")]
        [Authorize]
        public IActionResult Post(LoginCredentials loginCredentials)
        {
            AdoHelper ado = new AdoHelper();
           
            AuthorizedCredentials autorizado = new AuthorizedCredentials();

            try
            {

                DataRow drUsuario = ado.ExecDataSet("SELECT * FROM usuario where nombredeusuario=@nombreusuario and [contraseña] COLLATE Latin1_General_CS_AS=@contrasenia","@nombreusuario",
                    loginCredentials.NombreDeUsuario, "@contrasenia",loginCredentials.Contraseña ). Tables[0].Rows[0];
          
                    autorizado.ID = int.Parse(drUsuario["ID"].ToString());
                    autorizado.ID_DelCliente = int.Parse(drUsuario["ID_DelCliente"].ToString());
                    autorizado.Nombre = drUsuario["Nombre"].ToString();
                    autorizado.ApellidoPaterno = drUsuario["ApellidoPaterno"].ToString();
                    ////autorizado.ApellidoMaterno = drUsuario["ApellidoMaterno"].ToString();
                    ////autorizado.NombreDeUsuario = loginCredentials.NombreDeUsuario;
                    ////autorizado.Contraseña = loginCredentials.Contraseña;
                    autorizado.Email = drUsuario["CorreoElectrónico"].ToString();
                    ////autorizado.TeléfonoCelular = drUsuario["TeléfonoCelular"].ToString();
                    ////autorizado.TeléfonoAlterno = drUsuario["TeléfonoAlterno"].ToString();
                    autorizado.Rol = drUsuario["Rol"].ToString();        
               
                ado.Dispose();
                return Ok(autorizado);
            }
            catch
            {
                ado.Dispose();
                return BadRequest();
            }
        }
       
    }
}
