using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        UserManager<User> _usermanager;
        KanbanContext _dbcontext;

        public UserService(UserManager<User> usermanager, KanbanContext dbcontext)
        {
            _usermanager = usermanager;
            _dbcontext = dbcontext;
        }

        public async Task<ProfileModel> GetUserInfo(string name)
        {
            User user = await _usermanager.FindByNameAsync(name);

            if (user != null)
            {
                ProfileModel profile = new ProfileModel()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Age = user.Age,
                    AmountOfDoneIssues = user.AmountOfDoneIssues,
                    Phone = user.PhoneNumber
                };

                return profile;
            }
            else
            {
                throw new Exception("User not found");
            }

        }

        public async Task UpdateUser(ProfileModel model)
        {
            User user = await _usermanager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                user.Name = model.Name;
                user.Age = model.Age;
                user.PhoneNumber = model.Phone;

                await _usermanager.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Failed when updating");
            }
        }
    }
}
