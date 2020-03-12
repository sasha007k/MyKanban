using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class IssueRepository : Repository<Issue, int>, IIssueRepository
    {
        public IssueRepository(DbContext context) : base(context)
        {

        }
    }
}
