using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        public KanbanContext _dbcontext { get; set; }
        public UserManager<User> _usermanager { get; set; }
        public ProjectService(UserManager<User> usermanager, KanbanContext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._usermanager = usermanager;
        }

        public async Task<ProjectViewModel> GetProjectInfo(GetProjectInfoParams projectparams)
        {
            Project project = await _dbcontext.Set<Project>()
                .Include(p => p.User)
                .Include(p => p.Issues)
                .FirstOrDefaultAsync(p => p.Id == projectparams.ProjectId);

            if (project != null)
            {
                ProjectViewModel modeltoshow = new ProjectViewModel();

                if (projectparams.IsUserOnly)
                {
                    User user = await _usermanager.FindByNameAsync(projectparams.UserName);

                    if (user != null)
                    {
                        modeltoshow.ProjectId = project.Id;

                        modeltoshow.Name = project.Name;

                        modeltoshow.Description = project.Description;

                        var issues = await _dbcontext.Set<Issue>().Where(t => t.UserId == user.Id).ToListAsync();

                        modeltoshow.Issuess = issues;

                        return modeltoshow;
                    }
                    else
                    {
                        throw new Exception("User doesn't exist");
                    }
                }
                else
                {
                    modeltoshow.ProjectId = project.Id;

                    modeltoshow.Name = project.Name;

                    modeltoshow.Description = project.Description;

                    modeltoshow.Issuess = project.Issues;

                }

                return modeltoshow;

            }
            else
            {
                throw new Exception("Project doesn't exist");
            }
        }

        public async Task<List<User>> GetUsers(string currentUserName)
        {
            var user = await _usermanager.FindByNameAsync(currentUserName);
            var users = from u in _dbcontext.Users
                        where u.Id != user.Id
                        select u;
            return await users.ToListAsync();
        }

        public async Task<int> Add(ProjectAndSearchModel model)
        {
            var user = await _usermanager.FindByNameAsync(model.ProjectModel.OwnerName);
            model.ProjectModel.Users.Add(user);

            for (int i = 0; i < model.SearchUserModel.SelectedUsers.Count; i++)
            {
                var tempUser = await _usermanager.FindByIdAsync(model.SearchUserModel.SelectedUsers[i]);
                model.ProjectModel.Users.Add(tempUser);
            }

            var project = new Project()
            {
                Name = model.ProjectModel.Name,
                Description = model.ProjectModel.Description,
                UserId = Convert.ToInt32(user.Id)
            };


            await _dbcontext.Projects.AddAsync(project);

            await _dbcontext.SaveChangesAsync();

            if (model.ProjectModel.Users.Count != 0)
            {
                foreach (var item in model.ProjectModel.Users)
                {
                    var member = new Team()
                    {
                        ProjectId = project.Id,
                        UserId = Convert.ToInt32(item.Id)
                    };
                    await _dbcontext.Teams.AddAsync(member);
                }
                await _dbcontext.SaveChangesAsync();
            }


            return project.Id;
        }

        public async Task<ProjectsTable> GetAllProjects(ProjectSearchForTable projectparams)
        {
            int pageSize = 6;
            User user = await _usermanager.FindByNameAsync(projectparams.UserName);
            ProjectsTable projectsinfo = new ProjectsTable();
            if (user != null)
            {
                IEnumerable<ProjectModelToTable> projects = await _dbcontext.Set<Project>()
                    .Include(p => p.User).Include(p => p.Issues)
                    .Join((await _dbcontext.Set<Team>().ToListAsync()), p => p.Id, t => t.ProjectId, (p, t) => new ProjectModelToTable()
                    {
                        ProjectId = p.Id,
                        Name = p.Name,
                        Owner = p.User.Email,
                        Issues = p.Issues.Count(i => i.UserId == t.UserId.ToString()),
                        DoneIssues = p.Issues.Count(i => i.Status == Status.Done && i.UserId == t.UserId.ToString()),
                        UserIdWhoView = t.UserId.ToString()
                    })
                    .Where(p => p.UserIdWhoView == user.Id)
                    .ToListAsync();

                if (!String.IsNullOrEmpty(projectparams.Name))
                {
                    projects = projects.Where(p => p.Name.ToLower().Contains(projectparams.Name.ToLower()));
                }

                int projectsCount = projects.Count();
                if (projectparams.Page > (int)Math.Ceiling((decimal)projectsCount / pageSize) || projectparams.Page < 1)
                {
                    projectparams.Page = 1;
                }


                switch (projectparams.OrderBy)
                {
                    case "NameUp":
                        {
                            projects = projects.OrderBy(e => e.Name).ToList();
                            break;
                        }
                    case "NameDown":
                        {
                            projects = projects.OrderByDescending(e => e.Name).ToList();
                            break;
                        }
                    case "None":
                        {
                            projects = (projects).ToList().OrderBy(p => p.Percentage);
                            break;
                        }
                    default:
                        {
                            projects = (projects).ToList().OrderBy(p => p.Percentage);
                            break;
                        }
                }


                projectsinfo.projects = projects.Skip((projectparams.Page - 1) * pageSize).Take(pageSize).ToList(); ;
                projectsinfo.PageInfo = new PageInfo { PageNumber = projectparams.Page, PageSize = pageSize, TotalItems = projectsCount };
                return projectsinfo;
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
