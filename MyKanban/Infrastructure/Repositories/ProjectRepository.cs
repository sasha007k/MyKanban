using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project, int>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {

        }
    }
}
