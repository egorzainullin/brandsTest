using System.Threading;
using Xunit;

namespace ConsoleBrandsAppTest.Tests;

public class CoreTest
{
    private readonly Core _core;

    private const string Link = @"order_file.csv";

    private readonly CancellationTokenSource _source = new();
        
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
    public void GetPriceSum_NoOrders_ShouldReturnZero()
    {
        const string link = "order_empty_file.csv";
        var core = new Core(link, _source.Token);

        var price = core.GetPriceSum();

        Assert.Equal(new Cost(0, 0), price);
    }

    [Fact]
    public void GetMostPopular_SuccessPath_ShouldReturnCorrectBrandAndCategoryAndProduct()
    {
        var mostPopular = _core.GetMostPopular();
        
        Assert.Equal(new MostPopular("apple", "21", "2"), mostPopular);
    }

    [Fact]
    public void GetMostPopularBrand_WithMostPopularBrandEqualsEmptyString_ShouldReturnSecondMostPopularBrand()
    {
        const string link = "order_empty_brand.csv";
        var core = new Core(link, _source.Token);

        var mostPopularBrand = core.GetMostPopular().Brand;
        
        Assert.Equal("samsung", mostPopularBrand);
    }

    [Fact]
    public void GetMostPopularCategory_WithMostPopularCategoryEqualsEmptyString_ShouldReturnSecondMostPopularCategory()
    {
        const string link = "order_empty_category.csv";
        var core = new Core(link, _source.Token);

        var mostPopularCategory = core.GetMostPopular().Category;
        
        Assert.Equal("20", mostPopularCategory);
    }

    [Fact]
    public void GetMostPopularProduct_WithPostPopularProductEqualsEmptyString_ShouldReturnSecondMostPopularProduct()
    {
        const string link = "order_empty_product.csv";
        var core = new Core(link, _source.Token);

        var mostPopularProduct = core.GetMostPopular().Product;
        
        Assert.Equal("3", mostPopularProduct);
    }

    [Fact]
    public void GetMostPopular_WithEmptyFile_ShouldReturnNone()
    {
        const string link = "order_empty_file.csv";
        var core = new Core(link, _source.Token);

        var mostPopular = core.GetMostPopular();
        
        Assert.Equal(new MostPopular("None", "None", "None"), mostPopular);
    }
}