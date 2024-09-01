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
    public class DefaultStaticCommandsViewModel(ProductDatabaseService service) : LayoutViewModel
    {
        public GridViewDataSet<Product> Products { get; set; } = new();

        public override async Task PreRender()
        {
            Products = await service.LoadProducts(new GridViewDataSetOptions()
            {
                PagingOptions = new() { PageSize = 20 },
                SortingOptions = new() { SortExpression = nameof(Product.Name) }
            });

            await base.PreRender();
        }
    }

    public class ProductDatabaseService
    {
        [AllowStaticCommand]
        public async Task<GridViewDataSet<Product>> LoadProducts(GridViewDataSetOptions options)
        {
            var dataSet = new GridViewDataSet<Product>();
            dataSet.ApplyOptions(options);
            dataSet.LoadFromQueryable(ProductsDatabase.GetProducts());
            return dataSet;
        }
    }
}

