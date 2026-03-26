using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions;

public class GetAllAuctionsHandlerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly GetAllAuctionsQueryHandler _handler;
    
    public GetAllAuctionsHandlerTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _handler = new GetAllAuctionsQueryHandler(_auctionRepositoryMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfExistsAnyAuction()
    {
        // Arrange
        var query = new GetAllAuctionsQuery();
        List<Auction> auction = [new()
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            StartingBid = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        }];
        
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctions(CancellationToken.None))
            .Returns(auction);
        
        // Act
        var result = _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(auction, result.Value);
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfDoesNotExistsAnyAuction()
    {
        // Arrange
        var query = new GetAllAuctionsQuery();
        List<Auction>? auction = [];
        
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctions(CancellationToken.None))
            .Returns(auction);
        
        // Act
        var result = _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.NotFound");
        Assert.Contains(result.Errors, error => error.Code == "No auctions were found!");
    }
}