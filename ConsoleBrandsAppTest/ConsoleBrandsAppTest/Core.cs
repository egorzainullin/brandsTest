namespace ConsoleBrandsAppTest;

public static class Core
{
    public static Cost GetPriceSum(IEnumerable<ProductOrder> products)
    {
        long elementsAddedCount = 0;
        var sum = products
            .Select(x => x.Price)
            .Aggregate((x, acc) =>
            {
                ++elementsAddedCount;
                if (elementsAddedCount % 1000000 == 0)
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