using System.Collections;

namespace ConsoleBrandsAppTest;

public class ReaderToEnumerable(StreamReader reader, Dictionary<string, int> order) : IEnumerable<ProductOrder>
{
    public IEnumerator<ProductOrder> GetEnumerator()
    {
        return new ReaderEnumerator(reader, order);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class ReaderEnumerator(StreamReader reader, Dictionary<string, int> order) : IEnumerator<ProductOrder>
    {
        public bool MoveNext()
        {
            var line = reader.ReadLine();
            if (line == null)
            {
                return false;
            }

            Current = ProductOrder.ParseProduct(line, order);
            return true;
        }

        public void Reset()
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        public ProductOrder Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}