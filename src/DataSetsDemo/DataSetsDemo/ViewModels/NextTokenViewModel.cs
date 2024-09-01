using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSetsDemo.Services;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DataSetsDemo.ViewModels
{
    public class NextTokenViewModel(GitHubNextTokenService service) : LayoutViewModel
    {
        public GenericGridViewDataSet<IssueDto, NoFilteringOptions, SortingOptions, NextTokenPagingOptions, NoRowInsertOptions, RowEditOptions> Issues { get; set; } 
            = new(new(), new(), new(), new(), new());

        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                await Issues.LoadAsync(service.LoadIssues);
            }

            await base.PreRender();
        }
    }

    public class GitHubNextTokenService(GitHubService gitHubService)
    {
        [AllowStaticCommand]
        public async Task<GridViewDataSetResult<IssueDto, NoFilteringOptions, SortingOptions, NextTokenPagingOptions>> LoadIssues(GridViewDataSetOptions<NoFilteringOptions, SortingOptions, NextTokenPagingOptions> options)
        {
            var result = await gitHubService.GetGitHubIssues(options.PagingOptions.CurrentToken);
            options.PagingOptions.NextPageToken = result.nextToken;

            return new(result.items, options);
        }

        [AllowStaticCommand]
        public async Task<GridViewDataSetResult<IssueDto, NoFilteringOptions, SortingOptions, NextTokenPagingOptions>> LoadIssuesSlow(GridViewDataSetOptions<NoFilteringOptions, SortingOptions, NextTokenPagingOptions> options)
        {
            await Task.Delay(1000);

            var result = await gitHubService.GetGitHubIssues(options.PagingOptions.CurrentToken);
            options.PagingOptions.NextPageToken = result.nextToken;

            return new(result.items, options);
        }
    }
}

