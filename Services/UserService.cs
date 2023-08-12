using locationapi.Models;
using locationapi.Models.Common;
using locationapi.Models.Request;
using locationapi.Models.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace locationapi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings1 = new AppSettings();
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings1 = appSettings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            using (var db = new ZbGpsContext())
            {
                var usuario = db.Usuarios.Where(d=>d.NombreDeUsuario == model.Username
                && d.Contraseña == model.Password).FirstOrDefault();

                if (usuario == null) return null;

                userResponse.Username =usuario.NombreDeUsuario;
                userResponse.Token = GetToken(usuario);
            }
            return userResponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings1.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                        new Claim(ClaimTypes.Name,usuario.Nombre.ToString()),
                        new Claim("rol",usuario.Rol)
                    }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
