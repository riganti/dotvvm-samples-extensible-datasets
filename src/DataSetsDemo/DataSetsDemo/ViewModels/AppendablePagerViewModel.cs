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
    public class AppendablePagerViewModel(GitHubNextTokenService service) : LayoutViewModel
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
}

