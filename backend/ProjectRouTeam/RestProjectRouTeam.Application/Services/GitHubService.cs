using Newtonsoft.Json;
using RestProjectRouTeam.Core.Abstractions;
using RestProjectRouTeam.Core.Models;

namespace RestProjectRouTeam.Application.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly IGitHubProjectPageRepository _repository;

        public GitHubService(IGitHubProjectPageRepository repository)
        {
            _repository = repository;
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
        }

        public async Task<List<GitHubSubject>> SearchRepositoriesURL(string subject)
        {
            var response = await _httpClient.GetStringAsync($"https://api.github.com/search/repositories?q={subject}");

            dynamic result = JsonConvert.DeserializeObject(response);
            List<GitHubSubject> repositories = new List<GitHubSubject>();

            foreach (var item in result.items)
            {
                var jsonModel = new GitHubPage
                {
                    Name = item.name,
                    Author = item.owner.login,
                    StargazersCount = item.stargazers_count,
                    WatchersCount = item.watchers_count,
                    HtmlUrl = item.html_url
                };

                repositories.Add(new GitHubSubject
                {
                    SearchSubject = subject,
                    Json = JsonConvert.SerializeObject(jsonModel)
                });
            }

            return repositories;
        }

        public async Task<List<GitHubSubject>> SearchRepositoriesDB(string subject)
        {
            return await _repository.Search(subject);
        }

        public async Task<List<GitHubSubject>> Search(string subject)
        {
            var repositories = new List<GitHubSubject>();

            if (!string.IsNullOrEmpty(subject))
            {
                // Поиск в локальной БД  
                repositories = await SearchRepositoriesDB(subject);

                // поиск в https://api.github.com/search/repositories?q=subject
                if (repositories.Count == 0)
                {
                    repositories = await SearchRepositoriesURL(subject);

                    // сохранение результата в локальную БД
                    foreach (var repository in repositories)
                    {
                        await Create(repository);
                    }
                }

                return repositories;
            }

            return repositories;
        }

        #region CRUD  
        public async Task<GitHubSubject> Update(GitHubSubject entity)
        {
            return await _repository.Update(entity);
        }

        public async Task<GitHubSubject> Create(GitHubSubject entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<GitHubSubject>> Get()
        {
            return await _repository.Get();
        }

        public async Task<GitHubSubject> Get(int id)
        {
            return await _repository.Get(id);
        }
        #endregion
    }
}