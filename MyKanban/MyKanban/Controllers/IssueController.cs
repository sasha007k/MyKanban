using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyKanban.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private readonly IIssueService issueservice;

        public IssueController(IIssueService issueservice)
        {
            this.issueservice = issueservice;
        }

        public async Task<IActionResult> NewIssue(int projectId)
        {
            IssueAndSearchModel issueAndSearchModel = new IssueAndSearchModel();
            SearchUserModel user = new SearchUserModel
            {
                Users = await this.issueservice.GetUsersForTask(projectId),
            };
            issueAndSearchModel.SearchUserModel = user;
            issueAndSearchModel.ProjectId = projectId;
            return this.View(issueAndSearchModel);
        }

        public async Task<IActionResult> CreateNewIssue(IssueAndSearchModel model, int projectId)
        {
            if (model != null)
            {
                model.IssueModel.ProjectId = projectId;
                await this.issueservice.AddIssue(model);
            }

            return this.RedirectToAction("NewIssue", "Issue", new { projectId = projectId });
        }
    }
}