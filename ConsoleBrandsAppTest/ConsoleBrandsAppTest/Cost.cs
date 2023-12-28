namespace ConsoleBrandsAppTest;

public record struct Cost(long Dollars, long Cents)
{
    public static Cost ToCost(long cents)
    {
        var dollars = cents / 100;
        var remainderCents = cents % 100;
        return new Cost(dollars, remainderCents);
    }
}