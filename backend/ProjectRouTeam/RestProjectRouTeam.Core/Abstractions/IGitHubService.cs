using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.Core.Abstractions
{
    public interface IGitHubService  
    {
        Task<List<GitHubSubject>> SearchRepositoriesURL(string subject);
        Task<List<GitHubSubject>> SearchRepositoriesDB(string subject);

        Task<List<GitHubSubject>> Get();
        Task<GitHubSubject> Get(int id);
        Task<GitHubSubject> Create(GitHubSubject entity);
        Task<GitHubSubject> Update(GitHubSubject entity);
        Task<int> Delete(int id);

        Task<List<GitHubSubject>> Search(string subject);
    }
}