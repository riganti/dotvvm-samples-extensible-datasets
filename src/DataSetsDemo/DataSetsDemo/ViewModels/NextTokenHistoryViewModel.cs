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
    public class NextTokenHistoryViewModel(GitHubNextTokenHistoryService service) : LayoutViewModel
    {
        public GenericGridViewDataSet<IssueDto, NoFilteringOptions, SortingOptions, NextTokenHistoryPagingOptions, NoRowInsertOptions, RowEditOptions> Issues { get; set; } 
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

    public class GitHubNextTokenHistoryService(GitHubService gitHubService)
    {
        [AllowStaticCommand]
        public async Task<GridViewDataSetResult<IssueDto, NoFilteringOptions, SortingOptions, NextTokenHistoryPagingOptions>> LoadIssues(GridViewDataSetOptions<NoFilteringOptions, SortingOptions, NextTokenHistoryPagingOptions> options)
        {
            var result = await gitHubService.GetGitHubIssues(options.PagingOptions.GetCurrentPageToken());
            options.PagingOptions.SaveNextPageToken(result.nextToken);

            return new(result.items, options);
        }
    }
}

