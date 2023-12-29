namespace ConsoleBrandsAppTest;

public class StreamToProductOrder
{
    private readonly StreamReader _streamReader;

    public StreamToProductOrder(StreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public IEnumerable<ProductOrder> ToOrders()
    {
        var columns = _streamReader.ReadLine();
        var columnsArray = columns.Split(",");
        var dicOrder = new Dictionary<string, int>();
        for (int i = 0; i < columnsArray.Length; i++)
        {
            dicOrder[columnsArray[i]] = i;
        }

        for (var line = _streamReader.ReadLine(); line != null; line = _streamReader.ReadLine())
        {
            yield return ProductOrder.ParseProduct(line, dicOrder);
        }
    }
}