namespace ConsoleBrandsAppTest;

public class StreamToProductOrder(StreamReader streamReader, CancellationToken token)
{
    public IEnumerable<ProductOrder> ToOrders()
    {
        streamReader.DiscardBufferedData();
        streamReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
        var columns = streamReader.ReadLine();
        var columnsArray = columns.Split(",");
        var dicOrder = new Dictionary<string, int>();
        for (var i = 0; i < columnsArray.Length; ++i)
        {
            dicOrder[columnsArray[i]] = i;
        }

        for (var line = streamReader.ReadLine(); line != null; line = streamReader.ReadLine())
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            yield return ProductOrder.ParseProduct(line, dicOrder);
        }
    }
}