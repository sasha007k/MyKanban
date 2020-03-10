using Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<ProfileModel> GetUserInfo(string name);
        Task UpdateUser(ProfileModel model);
    }
}