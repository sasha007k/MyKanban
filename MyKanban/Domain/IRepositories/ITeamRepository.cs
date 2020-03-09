using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface ITeamRepository : IRepository<Team, int>
    {
    }
}