using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class IssueService : IIssueService
    {
        public KanbanContext _dbcontext { get; set; }
        public UserManager<User> _usermanager { get; set; }
        public IssueService(UserManager<User> usermanager, KanbanContext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._usermanager = usermanager;
        }

        public async Task<List<User>> GetUsersForTask(int projectId)
        {
            var usersId = from t in _dbcontext.Teams
                          where t.ProjectId == projectId
                          select t.UserId;

            var uniqueUsersId = usersId
                .Select(u => u)
                .Distinct();

            var users = new List<User>();
            foreach (var id in uniqueUsersId)
            {
                users.Add(await _usermanager.FindByIdAsync(id));
            }

            return users;
        }
        public async Task<bool> AddIssue(IssueAndSearchModel model)
        {
            var issue = new Issue()
            {
                Name = model.IssueModel.Name,
                Description = model.IssueModel.Description,
                UserId = model.SearchUserModel.SelectedUsers.First(),
                ProjectId = model.IssueModel.ProjectId,
                Status = Status.ToDo
            };

            await _dbcontext.Issues.AddAsync(issue);
            await _dbcontext.SaveChangesAsync();


            return true;
        }

        public async Task<List<Issue>> GetIssuseList(int ProjectId)
        {
            List<Issue> issues;

            issues = await (from i in _dbcontext.Issues
                            where i.ProjectId == ProjectId
                            select i).ToListAsync();



            return issues;
        }
    }
}