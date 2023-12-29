namespace ConsoleBrandsAppTest;

public record struct MostPopular(string Brand, string Category, string Product)
{
    public string Brand { get; } = Brand;

    public string Category { get; } = Category;

    public string Product { get; } = Product;
}