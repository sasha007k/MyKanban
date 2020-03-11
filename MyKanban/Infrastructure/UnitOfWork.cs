using Domain;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IIssueRepository issueRepository;
        private ITeamRepository teamRepository;
        private IUserRepository userRepository;
        private IProjectRepository projectRepository;

        //public IIssueRepository IssueRepository
        //{
        //    get
        //    {
        //        if (this.issueRepository == null)
        //        {
        //            issueRepository = new IssueRepository(_context);
        //        }

        //        return this.issueRepository;
        //    }
        //}
        //public ITeamRepository TeamRepository
        //{
        //    get
        //    {
        //        if (this.teamRepository == null)
        //        {
        //            teamRepository = new TeamRepository(_context);
        //        }

        //        return this.teamRepository;
        //    }
        //}
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }

                return this.userRepository;
            }
        }
        //public IProjectRepository ProjectRepository
        //{
        //    get
        //    {
        //        if (this.projectRepository == null)
        //        {
        //            projectRepository = new ProjectRepository(_context);
        //        }

        //        return this.projectRepository;
        //    }
        //}

        public DbContext _context { get; private set; }



        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public Task Commit()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
