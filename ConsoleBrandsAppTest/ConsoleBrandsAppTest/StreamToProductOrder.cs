namespace ConsoleBrandsAppTest;

/// <summary>
/// This class converts input from stream to products orders.
/// </summary>
public static class StreamToProductOrder
{
    /// <summary>
    /// Converts stream to produces orders.
    /// </summary>
    /// <param name="streamReader">Stream to read.</param>
    /// <param name="token">Token that cancels conversion.</param>
    /// <returns>Collection of product orders.</returns>
    public static IEnumerable<ProductOrder> ToOrders(StreamReader streamReader, CancellationToken token)
    {
        streamReader.DiscardBufferedData();
        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
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