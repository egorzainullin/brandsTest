namespace ConsoleBrandsAppTest;

/// <summary>
/// This class represents cost in dollars and cents.
/// </summary>
public record struct Cost(long Dollars, long Cents)
{
    /// <summary>
    /// Gets rounded dollars and cents from cents.
    /// </summary>
    /// <param name="cents">Cents those should be rounded.</param>
    /// <returns>Rounded cost.</returns>
    public static Cost ToCost(long cents)
    {
        var dollars = cents / 100;
        var remainderCents = cents % 100;
        return new Cost(dollars, remainderCents);
    }

    /// <summary>
    /// Operator that adds one cost to another. 
    /// </summary>
    /// <param name="cost1">First cost of sum.</param>
    /// <param name="cost2">Second cost of sum.</param>
    /// <returns>Sum of two costs.</returns>
    public static Cost operator +(Cost cost1, Cost cost2)
    {
        var dollarWithoutCents = cost1.Dollars + cost2.Dollars;
        var allCents = cost1.Cents + cost2.Cents;
        var dollarsAfterAddingCents = dollarWithoutCents + (allCents / 100);
        var cents = allCents % 100;
        return new Cost(dollarsAfterAddingCents, cents);
    }
}