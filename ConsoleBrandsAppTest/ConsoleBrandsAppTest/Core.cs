namespace ConsoleBrandsAppTest;

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

    public Cost GetPriceSum()
    {
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        long elementsAddedCount = 0;
        var sum = products
            .Select(x => x.Price)
            .Aggregate((acc, x) =>
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                ++elementsAddedCount;
                return x + acc;
            });
        return sum;
    }

    private static void UpdatePopularityDictionary(IDictionary<string, long> dic, string elementToAdd)
    {
        if (!dic.TryAdd(elementToAdd, 1))
        {
            ++dic[elementToAdd];
        }
    }

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

            long elementsProcessedCount = 0;
            ++elementsProcessedCount;
        }
    }

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

