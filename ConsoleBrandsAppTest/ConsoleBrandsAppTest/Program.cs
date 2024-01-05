namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    private static volatile object _objectToLock = new();

    public static void Main()
    {
        using var source = new CancellationTokenSource();
        var token = source.Token;
        var core = new Core(Link, token);
        Task.Run(() =>
        {
            var mostPopular = core.GetMostPopular();
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(mostPopular);
        }, token);
        Task.Run(() =>
        {
            var sum = core.GetPriceSum();
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(sum);
        }, token);

        var line = Console.ReadLine();
        if (line == "stop")
        {
            source.Cancel();
            Console.WriteLine("Tasks are stopped");
        }

        Console.ReadLine();
    }
}