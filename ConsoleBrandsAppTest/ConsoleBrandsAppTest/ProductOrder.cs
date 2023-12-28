namespace ConsoleBrandsAppTest;

public record struct ProductOrder
{
    public ProductOrder(Cost price, string productName, string category, string brand)
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

    private static string _categoryCode = "category_code";

    private static string _brand = "brand";

    private static string _price = "price";

    private static string _product = "product_id";

    public static ProductOrder ParseProduct(string toParse, Dictionary<string, int> order)
    {
        var columns = toParse.Split(",");
        var costStringToParse = columns[order[_price]];
        if (!Double.TryParse(costStringToParse, out var priceDouble))
        {
            throw new ArgumentException("Price can't be parsed as double.");
        }

        var priceInCents = (int)Math.Round(priceDouble * 100);
        var cost = Cost.ToCost(priceInCents);
        var productName = columns[order[_product]];
        var category = columns[order[_categoryCode]];
        var brand = columns[order[_brand]];
        return new ProductOrder(cost, productName, category, brand);
    }
}