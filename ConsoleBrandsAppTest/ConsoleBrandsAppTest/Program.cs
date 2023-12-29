// See https://aka.ms/new-console-template for more information

namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    public static void Main()
    {
        using var streamReader = new StreamReader(Link);
        var collectionStreamToProductOrder = new StreamToProductOrder(streamReader);
        var collection = collectionStreamToProductOrder.ToOrders();
        Core.GetPriceSum(collection);
    }
}