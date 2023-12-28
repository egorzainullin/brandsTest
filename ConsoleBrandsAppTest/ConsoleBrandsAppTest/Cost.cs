namespace ConsoleBrandsAppTest;

public record struct Cost(long Dollars, long Cents)
{
    public static Cost ToCost(long cents)
    {
        var dollars = cents / 100;
        var remainderCents = cents % 100;
        return new Cost(dollars, remainderCents);
    }

    public static Cost operator +(Cost cost1, Cost cost2)
    {
        var dollarWithoutCents = cost1.Dollars + cost2.Dollars;
        var allCents = cost1.Cents + cost2.Cents;
        var dollarsAfterAddingCents = dollarWithoutCents + (allCents / 100);
        var cents = allCents % 100;
        return new Cost(dollarsAfterAddingCents, cents);
    }
}