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
}