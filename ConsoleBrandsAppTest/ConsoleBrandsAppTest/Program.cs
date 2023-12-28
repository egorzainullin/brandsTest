// See https://aka.ms/new-console-template for more information

namespace ConsoleBrandsAppTest;

public static class Program
{
    private static string link = "2019-Oct.csv";

    public static void Main()
    {
        using (var streamReader = new StreamReader(link))
        {
            var columns = streamReader.ReadLine();
            var columnsArray = columns.Split(",");
            var dicOrder = new Dictionary<string, int>();
            for (int i = 0; i < columnsArray.Length; i++)
            {
                dicOrder[columnsArray[i]] = i;
                Console.WriteLine(columnsArray[i]);
            }
        }
    }
}