using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.Core.Abstractions
{
    public interface IGitHubProjectPageRepository
    {
        Task<List<GitHubSubject>> Get();
        Task<GitHubSubject> Get(int id);
        Task<GitHubSubject> Create(GitHubSubject entity);
        Task<GitHubSubject> Update(GitHubSubject entity);
        Task<int> Delete(int id);

        Task<List<GitHubSubject>> Search(string subject);
    }
}
