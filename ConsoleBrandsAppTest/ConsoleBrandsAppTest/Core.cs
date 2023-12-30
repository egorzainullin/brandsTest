namespace ConsoleBrandsAppTest;

public class Core(string link, CancellationToken token)
{
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
                long progressNotification = 1000000;
                if (elementsAddedCount % progressNotification == 0)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"Number of elements in sum {elementsAddedCount}");
                }

                return x + acc;
            });
        Console.SetCursorPosition(0, 1);
        Console.WriteLine($"Sum found {sum}");
        return sum;
    }

    public MostPopular GetMostPopular()
    {
        string mostPopularBrand = "None";
        long mostPopularBrandCount = 0;
        var popularityBrandDictionary = new Dictionary<string, long>();

        string mostPopularCategory = "None";
        long mostPopularCategoryCount = 0;
        var popularityCategoryDictionary = new Dictionary<string, long>();

        string mostPopularProduct = "None";
        long mostPopularProductCount = 0;
        var popularityProductDictionary = new Dictionary<string, long>();
        
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        foreach (var order in products)
        {
            if (!popularityBrandDictionary.TryAdd(order.Brand, 1))
            {
                ++popularityBrandDictionary[order.Brand];
            }

            if (!popularityCategoryDictionary.TryAdd(order.Category, 1))
            {
                ++popularityCategoryDictionary[order.Category];
            }

            if (!popularityProductDictionary.TryAdd(order.ProductName, 1))
            {
                ++popularityProductDictionary[order.ProductName];
            }
        }

        foreach (var (key, value) in popularityBrandDictionary)
        {
            if (key != String.Empty && value > mostPopularBrandCount)
            {
                mostPopularBrandCount = value;
                mostPopularBrand = key;
            }
        }

        foreach (var (key, value) in popularityCategoryDictionary)
        {
            if (key != String.Empty && value > mostPopularCategoryCount)
            {
                mostPopularCategoryCount = value;
                mostPopularCategory = key;
            }
        }

        foreach (var (key, value) in popularityProductDictionary)
        {
            if (key != String.Empty && value > mostPopularProductCount)
            {
                mostPopularProductCount = value;
                mostPopularProduct = key;
            }
        }

        Console.WriteLine($"Brand: {mostPopularBrand}: {mostPopularBrandCount}");
        Console.WriteLine($"Category: {mostPopularCategory}: {mostPopularCategoryCount}");
        Console.WriteLine($"Product: {mostPopularProduct}: {mostPopularProductCount}");
        return new MostPopular(mostPopularBrand, mostPopularCategory, mostPopularProduct);
    }
}