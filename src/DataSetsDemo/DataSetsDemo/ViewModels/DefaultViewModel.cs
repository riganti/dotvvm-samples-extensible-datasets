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
    public class DefaultViewModel : LayoutViewModel
    {

        public GridViewDataSet<Product> Products { get; set; } = new()
        {
            PagingOptions = { PageSize = 20 },
            SortingOptions = { SortExpression = nameof(Product.Name) }
        };

        public override Task PreRender()
        {
            IQueryable<Product> queryable = ProductsDatabase.GetProducts();
            Products.LoadFromQueryable(queryable);

            return base.PreRender();
        }
    }
}

