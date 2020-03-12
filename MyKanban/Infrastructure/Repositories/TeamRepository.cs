using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team, int>, ITeamRepository
    {
        public TeamRepository(DbContext context) : base(context)
        {

        }
    }
}
