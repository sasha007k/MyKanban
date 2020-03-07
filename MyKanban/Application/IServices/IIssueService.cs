using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IIssueService
    {
        Task<List<User>> GetUsersForTask(int projectId);
        Task<bool> AddIssue(IssueAndSearchModel model);
        Task<List<Issue>> GetIssuseList(int ProjectId);
    }
}
