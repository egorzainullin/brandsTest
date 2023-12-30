// See https://aka.ms/new-console-template for more information

namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    public static void Main()
    {
        using var source = new CancellationTokenSource();
        var token = source.Token;

        var core = new Core(Link, token);
        Console.WriteLine(core.GetMostPopular());

        // Task.Run(() =>
        // {
        //     var streamToProductOrder = new StreamToProductOrder(streamReader1, token);
        //     var collection = streamToProductOrder.ToOrders();
        //     Core.GetPriceSum(collection, token);
        // }, token);
        // var line = Console.ReadLine();
        // const string stopWord = "stop";
        // if (line == stopWord)
        // {
        //     source.Cancel();
        //     Console.WriteLine("Tasks are stopped");
        // }
        //
        // while (line != String.Empty)
        // {
        //     line = Console.ReadLine();
        // }
    }
}