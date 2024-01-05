using System.Threading;
using ConsoleBrandsAppTest;
using Xunit;

namespace ConsoleBrandsAppTest.Tests;

public class CoreTest
{
    private readonly Core _core;

    private const string Link = @"order_file.csv";

    private readonly CancellationTokenSource _source = new CancellationTokenSource();
        
    public CoreTest()
    {
        var token = _source.Token;
        _core = new Core(Link, token);
    }
    
    [Fact]
    public void GetPriceSum_SuccessPath_ShouldReturnCorrectSum()
    {
        var price = _core.GetPriceSum();
        
        Assert.Equal(new Cost(88, 20), price);
    }

    [Fact]
    public void GetMostPopular_SuccessPath_ShouldReturnCorrectBrandAndCategoryAndProduct()
    {
        var mostPopular = _core.GetMostPopular();
        
        Assert.Equal(new MostPopular("apple", "21", "2"), mostPopular);
    }
    
}