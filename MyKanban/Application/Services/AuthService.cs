using Application.IServices;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        UserManager<User> _usermanager;
        SignInManager<User> _signinmanager;

        public AuthService(UserManager<User> usermanager, SignInManager<User> signinmanager)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }

        public async Task SignOut()
        {
            await _signinmanager.SignOutAsync();
        }

        public async Task<SignInResult> SignIn(LoginModel model)
        {
            return await _signinmanager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            User user = new User();
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Name = "No name";

            IdentityResult result = await _usermanager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _usermanager.AddToRoleAsync(user, "Worker");
                await _signinmanager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            return result;

        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await _usermanager.FindByNameAsync(name);
        }
    }
}
