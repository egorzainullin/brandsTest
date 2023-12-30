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
        string mostPopularBrand = null;
        long mostPopularBrandCount = 0;
        long i = -1;
        using var streamReader = new StreamReader(link);
        var products = StreamToProductOrder.ToOrders(streamReader, token);
        foreach (var order in products)
        {
            ++i;
            long currentBrandCount = 1;
            var currentBrand = order.Brand;
            var toSkip = i;
            using var innerStreamReader = new StreamReader(link);
            var innerProducts = StreamToProductOrder.ToOrders(innerStreamReader, token);
            foreach (var nextOrder in innerProducts)
            {
                if (toSkip >= 0)
                {
                    --toSkip;
                    continue;
                }

                if (currentBrand == nextOrder.Brand)
                {
                    ++currentBrandCount;
                }
            }

            
            Console.SetCursorPosition(0, 2);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Current brand {currentBrand} {currentBrandCount}");
            
            if (currentBrandCount > mostPopularBrandCount)
            {
                mostPopularBrandCount = currentBrandCount;
                mostPopularBrand = currentBrand;
            }
        }

        return new MostPopular(mostPopularBrand, "", "");
    }
}