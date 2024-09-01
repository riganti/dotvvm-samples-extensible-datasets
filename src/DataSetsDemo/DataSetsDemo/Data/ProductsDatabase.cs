using Bogus;

namespace DataSetsDemo.Data;

public class ProductsDatabase
{
    private static readonly List<Product> products;

    static ProductsDatabase()
    {
        var generator = new Faker<Product>()
            .RuleFor(p => p.Id, r => r.IndexGlobal)
            .RuleFor(p => p.Name, r => r.Commerce.ProductName())
            .RuleFor(p => p.Description, r => r.Commerce.ProductDescription())
            .RuleFor(p => p.ImageUrl, r => r.Image.PicsumUrl())
            .RuleFor(p => p.InStock, r => r.Random.Bool())
            .RuleFor(p => p.Price, r => r.Random.Decimal(1, 1000));
        products = generator.Generate(100);
    }

    public static IQueryable<Product> GetProducts()
    {
        return products.AsQueryable();
    }
}