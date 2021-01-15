using System;
using System.Threading.Tasks;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services
{
    public interface IAuthenticateService
    {
        Task<string> LoginUser(LoginUser loginUser);

        Task<string> RegisterUser(RegisterUser registerUser);
    }
}
