namespace ConsoleBrandsAppTest;

/// <summary>
/// Main class that calculates all cost, most popular brand, category and product.
/// </summary>
/// <param name="link">Link to file from which program gets data.</param>
/// <param name="token">Token which is supposed to stop calculation.</param>
public class Core(string link, CancellationToken token)
{
    private string _mostPopularBrand = "None";
    private long _mostPopularBrandCount = 0;
    private readonly IDictionary<string, long> _popularityBrandDictionary = new Dictionary<string, long>();

    private string _mostPopularCategory = "None";
    private long _mostPopularCategoryCount = 0;
    private readonly IDictionary<string, long> _popularityCategoryDictionary = new Dictionary<string, long>();

    private string _mostPopularProduct = "None";
    private long _mostPopularProductCount = 0;
    private readonly IDictionary<string, long> _popularityProductDictionary = new Dictionary<string, long>();

    /// <summary>
    ///  Gets all cost from orders.
    /// </summary>
    /// <returns>Cost in dollars and cents.</returns>
    public Cost GetPriceSum()
    {
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        var sum = new Cost();
        foreach (var productOrder in products)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            sum += productOrder.Price;
        }
        return sum;
    }

    /// <summary>
    /// Adds one if it contains in current element.
    /// </summary>
    /// <param name="dic">Dictionary to add.</param>
    /// <param name="elementToAdd">Element to which one should be added.</param>
    private static void UpdatePopularityDictionary(IDictionary<string, long> dic, string elementToAdd)
    {
        if (!dic.TryAdd(elementToAdd, 1))
        {
            ++dic[elementToAdd];
        }
    }

    /// <summary>
    /// Calculates popularity of brand, category and product. 
    /// </summary>
    /// <param name="products">Products to process.</param>
    private void CalculatePopularity(IEnumerable<ProductOrder> products)
    {
        foreach (var order in products)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            UpdatePopularityDictionary(_popularityBrandDictionary, order.Brand);
            UpdatePopularityDictionary(_popularityCategoryDictionary, order.Category);
            UpdatePopularityDictionary(_popularityProductDictionary, order.ProductName);
        }
    }

    /// <summary>
    /// Get most popular brand, category or product from popularity dictionary.
    /// </summary>
    /// <param name="dic">Dictionary to process.</param>
    /// <returns>Tuple of most popular element and its count in dictionary.</returns>
    private (string mostPopular, long count) GetMostPopularAndCount(IDictionary<string, long> dic)
    {
        var mostPopular = "None";
        long count = 0;
        foreach (var (key, value) in dic)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            if (key != String.Empty && value > count)
            {
                mostPopular = key;
                count = value;
            }
        }

        return (mostPopular, count);
    }

    /// <summary>
    /// Gets most popular brand, category and product. 
    /// </summary>
    /// <returns>Most popular brand, category and product.</returns>
    public MostPopular GetMostPopular()
    {
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        CalculatePopularity(products);
        (_mostPopularBrand, _mostPopularBrandCount) = GetMostPopularAndCount(_popularityBrandDictionary);
        (_mostPopularCategory, _mostPopularCategoryCount) = GetMostPopularAndCount(_popularityCategoryDictionary);
        (_mostPopularProduct, _mostPopularProductCount) = GetMostPopularAndCount(_popularityProductDictionary);
        return new MostPopular(_mostPopularBrand, _mostPopularCategory, _mostPopularProduct);
    }
}

