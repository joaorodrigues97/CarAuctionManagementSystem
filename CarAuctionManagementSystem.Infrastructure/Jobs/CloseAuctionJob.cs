using CarAuctionManagementSystem.Domain.Auctions;
using Quartz;

namespace CarAuctionManagementSystem.Infrastructure.Jobs;

public class CloseAuctionJob(IAuctionRepository auctionRepository) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        var jobData = context.MergedJobDataMap;

        string? vin = jobData.GetString("vin");

        Auction? auction = auctionRepository.GetByVin(vin);

        if (auction is not null)
        {
            Auction.StopAuction(auction);
        }
        
        return Task.CompletedTask;
    }
}