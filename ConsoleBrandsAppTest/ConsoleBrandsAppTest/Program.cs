// See https://aka.ms/new-console-template for more information

namespace ConsoleBrandsAppTest;

public static class Program
{
    private const string Link = "2019-Oct.csv";

    public static void Main()
    {
        using (var streamReader = new StreamReader(Link))
        {
            var columns = streamReader.ReadLine();
            var columnsArray = columns.Split(",");
            var dicOrder = new Dictionary<string, int>();
            for (int i = 0; i < columnsArray.Length; i++)
            {
                dicOrder[columnsArray[i]] = i;
            }

            var collection = new ReaderToEnumerable(streamReader, dicOrder);
            Core.GetPriceSum(collection);
        }
    }
}