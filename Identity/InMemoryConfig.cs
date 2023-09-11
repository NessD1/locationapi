using IdentityModel;
using System.Data;
using System.Security.Claims;

namespace locationapi.Identity
{
    public static class InMemoryConfig
    {

        public static List<User> GetUsers()         
        {
            AdoHelper ado = new AdoHelper();
            DataTable dtUsuarios = ado.ExecDataSet("SELECT [SubjectId] ,[NombreDeUsuario] ,[Contraseña] ,[Nombre] ,[ApellidoPaterno],[Rol]   FROM [dbo].[Usuario]").Tables[0];
            List<User> usuarios = new List<User>();

            foreach (DataRow dr in dtUsuarios.Rows)
            {
                usuarios.Add(new User()
                {
                    SubjectId = dr["subjectid"].ToString(),
                    Username = dr["nombredeusuario"].ToString(),
                    Password = dr["contraseña"].ToString(),
                    Claims = new List<Claim>()
                    {
                        new Claim("given_name",dr["nombre"].ToString()),
                        new Claim("family_name",dr["apellidopaterno"].ToString()),
                        new Claim("rol", dr["rol"].ToString()),

                    }
                });
            }              
              return usuarios;
        }

    }
}
