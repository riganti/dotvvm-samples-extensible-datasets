using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSetsDemo.Data;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DataSetsDemo.ViewModels
{
    public class MultiSortViewModel(ProductMultiSortDatabaseService service) : LayoutViewModel
    {
        public MultiSortDataSet<Product> Products { get; set; } = new()
        {
            PagingOptions = { PageSize = 20 },
            SortingOptions =
            {
                Criteria =
                [
                    new SortCriterion() { SortExpression = nameof(Product.InStock) },
                    new SortCriterion() { SortExpression = nameof(Product.Name) }
                ]
            }
        };

        public override async Task PreRender()
        {
            Products = await service.LoadProducts(Products.GetOptions());

            await base.PreRender();
        }
    }

    public class MultiSortDataSet<T>()
        : GenericGridViewDataSet<T, NoFilteringOptions, MultiCriteriaSortingOptions, PagingOptions, RowInsertOptions<T>, RowEditOptions>(new(), new(), new(), new(), new())
        where T : class, new()
    {
    }

    public class ProductMultiSortDatabaseService
    {
        [AllowStaticCommand]
        public async Task<MultiSortDataSet<Product>> LoadProducts(GridViewDataSetOptions<NoFilteringOptions, MultiCriteriaSortingOptions, PagingOptions> options)
        {
            var dataSet = new MultiSortDataSet<Product>();
            dataSet.ApplyOptions(options);
            dataSet.LoadFromQueryable(ProductsDatabase.GetProducts());
            return dataSet;
        }
    }
}

