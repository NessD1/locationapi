using locationapi.Models.Request;
using locationapi.Models.Response;

namespace locationapi.Services    
{
    public interface IUserService 
    {        
        UserResponse Auth(AuthRequest model);
    }
}
