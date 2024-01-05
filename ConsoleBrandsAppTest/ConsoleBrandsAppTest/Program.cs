namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    public static void Main()
    {
        using var source = new CancellationTokenSource();
        var token = source.Token;
        var core = new Core(Link, token);
        Task.Run(() => Console.WriteLine(core.GetMostPopular()), token);
        Task.Run(() => Console.WriteLine(core.GetPriceSum()), token);

        var line = Console.ReadLine();
        if (line == "stop")
        {
            source.Cancel();
            Console.WriteLine("Tasks are stopped");
        }

        Console.ReadLine();
    }
}