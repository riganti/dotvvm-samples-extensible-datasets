using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace DataSetsDemo.Services;

public class GitHubService
{
    private readonly HttpClient client;

    public GitHubService(HttpClient client)
    {
        this.client = client;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
    }

    public async Task<(IssueDto[] items, string nextToken)> GetGitHubIssues(string currentToken)
    {
        var response = await client.GetAsync(currentToken ?? "https://api.github.com/repos/riganti/dotvvm/issues");
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<IssueDto[]>();

        return (items, ParseNextToken(response));
    }
    
    private string ParseNextToken(HttpResponseMessage response)
    {
        var linkHeader = response.Headers.GetValues("Link").FirstOrDefault();
        if (string.IsNullOrWhiteSpace(linkHeader))
        {
            return null;
        }

        var match = Regex.Match(linkHeader, @"<([^>]+)>; rel=""next""");
        return match.Success ? match.Groups[1].Value : null;
    }
}

public class IssueDto
{
    public long Id { get; set; }
    public long Number { get; set; }
    public string Title { get; set; }
    public string State { get; set; }
}
