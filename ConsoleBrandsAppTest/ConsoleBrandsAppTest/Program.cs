// See https://aka.ms/new-console-template for more information

namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    public static void Main()
    {
        using var streamReader = new StreamReader(Link);
        var source = new CancellationTokenSource();
        var token = source.Token;
        var task1 = Task.Run(() =>
        {
            var collectionStreamToProductOrder = new StreamToProductOrder(streamReader, token);
            var collection = collectionStreamToProductOrder.ToOrders(token);
            Core.GetPriceSum(collection, token);
        }, token);
        Thread.Sleep(2000);
        source.Cancel();
    }
}