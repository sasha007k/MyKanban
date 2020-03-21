﻿using System.Threading.Tasks;
using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAspServer.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectservice;
        private readonly IIssueService issueservice;

        public ProjectController(IProjectService projectservice, IIssueService issueservice)
        {
            this.projectservice = projectservice;
            this.issueservice = issueservice;
        }

        public async Task<IActionResult> New()
        {
            ProjectAndSearchModel projectAndSearchModel = new ProjectAndSearchModel();
            var currentUserName = this.User.Identity.Name;
            SearchUserModel user = new SearchUserModel
            {
                Users = await this.projectservice.GetUsers(currentUserName),
            };
            projectAndSearchModel.SearchUserModel = user;
            return this.View(projectAndSearchModel);
        }

        // [HttpPost]
        // public IActionResult New()
        // {
        //    return this.View();
        // }
        public async Task<IActionResult> NewProject(ProjectAndSearchModel model)
        {
            var projectParams = new GetProjectInfoParams();
            if (model != null)
            {
                model.ProjectModel.OwnerName = this.User.Identity.Name;
                projectParams.ProjectId = await this.projectservice.Add(model);
            }

            return this.RedirectToAction("AllProjects", projectParams);
        }

     
    }
}