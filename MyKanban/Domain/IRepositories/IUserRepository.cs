using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
    }
}