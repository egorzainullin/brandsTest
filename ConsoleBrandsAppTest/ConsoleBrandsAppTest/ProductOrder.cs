using System.Globalization;

namespace ConsoleBrandsAppTest;

/// <summary>
/// Class that represents order.
/// </summary>
public record struct ProductOrder
{
    private ProductOrder(Cost price, string productName, string category, string brand)
    {
        this.Price = price;
        this.ProductName = productName;
        this.Category = category;
        this.Brand = brand;
    }

    public Cost Price { get; }

    public string ProductName { get; }

    public string Category { get; }

    public string Brand { get; }

    private static string _category = "category_id";

    private static string _brand = "brand";

    private static string _price = "price";

    private static string _product = "product_id";

    /// <summary>
    /// Parses string to order by using given match.
    /// </summary>
    /// <param name="toParse">String to parse.</param>
    /// <param name="order">given match.</param>
    /// <returns>Product order which matches given string.</returns>
    /// <exception cref="ArgumentException">Throws when price is not correct.</exception>
    public static ProductOrder ParseProduct(string toParse, Dictionary<string, int> order)
    {
        var columns = toParse.Split(",");
        var costStringToParse = columns[order[_price]];
        if (!Double.TryParse(costStringToParse, CultureInfo.InvariantCulture, out var priceDouble))
        {
            throw new ArgumentException("Price can't be parsed as double.");
        }

        var priceInCents = (int)Math.Round(priceDouble * 100);
        var cost = Cost.ToCost(priceInCents);
        var productName = columns[order[_product]];
        var category = columns[order[_category]];
        var brand = columns[order[_brand]];
        return new ProductOrder(cost, productName, category, brand);
    }
}