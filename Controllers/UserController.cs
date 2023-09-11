using locationapi.Models.Request;
using locationapi.Models.Response;
using locationapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace locationapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [HttpPost("login")]        
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userresponse = _userService.Auth(model);
            if (userresponse == null) {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta.";
                return BadRequest(respuesta);
               };

            respuesta.Exito = 1;
            respuesta.Datos = userresponse;
            return Ok(respuesta);
        }
    }
}
