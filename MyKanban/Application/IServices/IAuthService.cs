using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthService
    {
        Task<SignInResult> SignIn(LoginModel model);
        Task SignOut();
        Task<IdentityResult> Register(RegisterModel model);
        Task<User> FindByNameAsync(string name);
    }
}
