namespace ConsoleBrandsAppTest;

/// <summary>
/// Main class that calculates all cost, most popular brand, category and product.
/// </summary>
/// <param name="link">Link to file from which program gets data.</param>
/// <param name="token">Token which is supposed to stop calculation.</param>
public class Core(string link, CancellationToken token)
{
    private string _mostPopularBrand = "None";
    private readonly IDictionary<string, long> _popularityBrandDictionary = new Dictionary<string, long>();

    private string _mostPopularCategory = "None";
    private readonly IDictionary<string, long> _popularityCategoryDictionary = new Dictionary<string, long>();

    private string _mostPopularProduct = "None";
    private readonly IDictionary<string, long> _popularityProductDictionary = new Dictionary<string, long>();

    /// <summary>
    ///  Gets all cost from orders.
    /// </summary>
    /// <returns>Cost in dollars and cents.</returns>
    public Cost GetPriceSum()
    {
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        var sum = new Cost(0, 0);
        long elementsProcessedCount = 0;
        foreach (var productOrder in products)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            sum += productOrder.Price;
            ++elementsProcessedCount;
            UpdateProgressSum(elementsProcessedCount);
        }
        return sum;
    }

    private static void UpdateProgressSum(long current)
    {
        if (current % 1_000_000 == 0)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write($"Elements in sum processed {current}");
        }
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
        long elementProcessedCount = 0;
        foreach (var order in products)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            UpdatePopularityDictionary(_popularityBrandDictionary, order.Brand);
            UpdatePopularityDictionary(_popularityCategoryDictionary, order.Category);
            UpdatePopularityDictionary(_popularityProductDictionary, order.ProductName);
            ++elementProcessedCount;
            UpdateProgressOfMostPopular(elementProcessedCount);
        }
    }
    
    private static void UpdateProgressOfMostPopular(long current)
    {
        if (current % 1_000_000 == 0)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write($"Elements in popularity processed {current}");
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
        (_mostPopularBrand, _) = GetMostPopularAndCount(_popularityBrandDictionary);
        (_mostPopularCategory, _) = GetMostPopularAndCount(_popularityCategoryDictionary);
        (_mostPopularProduct, _) = GetMostPopularAndCount(_popularityProductDictionary);
        return new MostPopular(_mostPopularBrand, _mostPopularCategory, _mostPopularProduct);
    }
}

