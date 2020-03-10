using Application.Models;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IProjectService
    {
        Task<List<User>> GetUsers(string currentUserName);
    }
    //Task<ProjectViewModel> GetProjectInfo(GetProjectInfoParams projectparams);
    //Task<int> Add(ProjectAndSearchModel model);
    //Task<ProjectsTable> GetAllProjects(ProjectSearchForTable projectparams);
}
