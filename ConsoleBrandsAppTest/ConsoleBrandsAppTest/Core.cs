namespace ConsoleBrandsAppTest;

public class Core
{
    public static Cost GetPriceSum(IEnumerable<ProductOrder> products)
    {
        var sum = products
            .Select(x => x.Price)
            .Aggregate((x, acc) => x + acc);
        return sum;
    }
}