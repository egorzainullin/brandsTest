namespace ConsoleBrandsAppTest;

public class StreamToProductOrder
{
    private readonly StreamReader _streamReader;
    
    private readonly CancellationToken _token;

    public StreamToProductOrder(StreamReader streamReader, CancellationToken token)
    {
        _streamReader = streamReader;
        _token = token;
    }

    public IEnumerable<ProductOrder> ToOrders(CancellationToken token)
    {
        var columns = _streamReader.ReadLine();
        var columnsArray = columns.Split(",");
        var dicOrder = new Dictionary<string, int>();
        for (var i = 0; i < columnsArray.Length; ++i)
        {
            dicOrder[columnsArray[i]] = i;
        }

        for (var line = _streamReader.ReadLine(); line != null; line = _streamReader.ReadLine())
        {
            if (_token.IsCancellationRequested)
            {
                _token.ThrowIfCancellationRequested();
            }
            yield return ProductOrder.ParseProduct(line, dicOrder);
        }
    }
}