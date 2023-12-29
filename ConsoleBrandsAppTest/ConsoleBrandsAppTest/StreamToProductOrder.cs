namespace ConsoleBrandsAppTest;

public class StreamToProductOrder(StreamReader streamReader)
{
    public IEnumerable<ProductOrder> ToOrders()
    {
        var columns = streamReader.ReadLine();
        var columnsArray = columns.Split(",");
        var dicOrder = new Dictionary<string, int>();
        for (var i = 0; i < columnsArray.Length; ++i)
        {
            dicOrder[columnsArray[i]] = i;
        }

        for (var line = streamReader.ReadLine(); line != null; line = streamReader.ReadLine())
        {
            yield return ProductOrder.ParseProduct(line, dicOrder);
        }
    }
}