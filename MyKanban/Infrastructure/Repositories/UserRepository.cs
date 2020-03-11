using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
