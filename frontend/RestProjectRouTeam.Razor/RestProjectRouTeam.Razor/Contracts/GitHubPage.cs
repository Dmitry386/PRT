namespace RestProjectRouTeam.Razor.Contracts
{
    /// <summary>
    /// JSON MODEL
    /// </summary>
    public class GitHubPage
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int StargazersCount { get; set; }
        public int WatchersCount { get; set; }
        public string HtmlUrl { get; set; }
    }
}