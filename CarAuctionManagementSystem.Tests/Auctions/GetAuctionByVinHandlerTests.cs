using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AuctionsByVin;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions;

public class GetAuctionByVinHandlerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly GetAuctionByVinQueryHandler _handler;
    
    public GetAuctionByVinHandlerTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _handler = new GetAuctionByVinQueryHandler(_auctionRepositoryMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfExistsAnyAuctionForTheVin()
    {
        // Arrange
        var query = new GetAuctionByVinQuery("safsdfsdsf");
        Auction auction = new()
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            Reserve = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        };
        
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetByVin(query.Vin, CancellationToken.None))
            .Returns(auction);
        
        // Act
        var result = _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(auction, result.Value);
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfDoesNotExistsAnyAuctionForTheVin()
    {
        // Arrange
        var query = new GetAuctionByVinQuery("safsdfsdsf");
        Auction? auction = null;
        
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetByVin(query.Vin, CancellationToken.None))
            .Returns(auction);
        
        // Act
        var result = _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.NotFound");
        Assert.Contains(result.Errors, error => error.Name == "No auctions were found!");
    }
}