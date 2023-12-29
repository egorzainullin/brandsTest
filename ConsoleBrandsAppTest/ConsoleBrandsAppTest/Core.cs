namespace ConsoleBrandsAppTest;

public static class Core
{
    public static Cost GetPriceSum(IEnumerable<ProductOrder> products, CancellationToken token)
    {
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

    public static MostPopular GetMostPopular(IEnumerable<ProductOrder> products, CancellationToken token)
    {
        string mostPopularBrand = null;
        long mostPopularBrandCount = 0;
        long i = -1;
        foreach (var order in products)
        {
            ++i;
            long currentBrandCount = 1;
            var currentBrand = order.Brand;
            var toSkip = i;
            foreach (var nextOrder in products)
            {
                if (toSkip > 0)
                {
                    --toSkip;
                    continue;
                }

                if (currentBrand == nextOrder.Brand)
                {
                    ++currentBrandCount;
                }
            }

            if (currentBrandCount > mostPopularBrandCount)
            {
                mostPopularBrandCount = currentBrandCount;
                mostPopularBrand = currentBrand;
            }
        }

        return new MostPopular(mostPopularBrand, "", "");
    }
}