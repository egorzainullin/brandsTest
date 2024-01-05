namespace ConsoleBrandsAppTest;

/// <summary>
/// This class represents most popular brand, category and product.
/// </summary>
public readonly record struct MostPopular(string Brand, string Category, string Product)
{
    public string Brand { get; } = Brand;

    public string Category { get; } = Category;

    public string Product { get; } = Product;
}