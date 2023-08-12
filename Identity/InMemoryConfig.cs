using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Data;
using System.Security.Claims;

namespace locationapi.Identity
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()            
          };

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

        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
           new Client
           {
                ClientId = "zartbit",
                ClientSecrets = new [] { new Secret("zbNessd1Lstryke".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile                    
               },                
            }
        };

    }
}
