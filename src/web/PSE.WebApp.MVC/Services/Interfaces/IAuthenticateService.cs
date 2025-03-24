using PSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Services.Interfaces;

public interface IAuthenticateService
{
    Task<UserLoginTokenResponse> LoginUser(LoginUser loginUser);

    Task<UserLoginTokenResponse> RegisterUser(RegisterUser registerUser);

    Task ConnectAccount(UserLoginTokenResponse userLoginTokenResponse);

    Task Logout();

    bool TokenExpired();

    Task<bool> RefreshTokenIsValid();
}