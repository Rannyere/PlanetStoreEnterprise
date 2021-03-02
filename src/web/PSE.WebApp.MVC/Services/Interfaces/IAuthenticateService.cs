﻿using System;
using System.Threading.Tasks;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<UserLoginTokenResponse> LoginUser(LoginUser loginUser);

        Task<UserLoginTokenResponse> RegisterUser(RegisterUser registerUser);
    }
}