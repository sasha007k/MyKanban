using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork
    {
       // IIssueRepository IssueRepository { get; }
       // ITeamRepository TeamRepository { get; }
      //  IUserRepository UserRepository { get; }
      //  IProjectRepository ProjectRepository { get; }

        Task Commit();
    }
}