using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Abstractions.Utils;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using CarAuctionManagementSystem.Infrastructure.Jobs;
using FluentValidation;
using Quartz;

namespace CarAuctionManagementSystem.Application.Auctions.StartAuction;

public sealed class StartAuctionCommandHandler(IAuctionRepository auctionRepository,
                                               IVehicleRepository vehicleRepository,
                                               ISchedulerFactory schedulerFactory,
                                               IValidator<StartAuctionCommand> validator) : ICommandHandler<StartAuctionCommand, bool>
{
    public Result<bool> Handle(StartAuctionCommand command, 
                                           CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return Result.Failure<bool>(validationResult.Errors.ConvertToError());
        }
        
        var vehicleByVin = vehicleRepository.GetVehicleByVin(command.Vin!);

        if (vehicleByVin is null)
        {
            return Result.Failure<bool>([AuctionErrors.VehicleNotExistsConflict]);
        }

        var auctionByVin = auctionRepository.GetAuctionByVin(command.Vin!);

        if (auctionByVin is not null)
        {
            return Result.Failure<bool>([AuctionErrors.AuctionConflict]);
        }

        ScheduleCloseAuctionJob(command.Vin, cancellationToken);
        
        Auction auction = new Auction
        {
            Vin = command.Vin,
            StartingBid = vehicleByVin.StartingBid
        };

        var result = auctionRepository.StartAuction(auction, cancellationToken);

        return Result.Create(result);
    }
    
    private void ScheduleCloseAuctionJob(string vin, CancellationToken cancellationToken)
    {
        var scheduler = schedulerFactory.GetScheduler(cancellationToken);

        var jobData = new JobDataMap
        {
            { "vin", vin }
        };
        
        var job = JobBuilder.Create<CloseAuctionJob>()
            .WithIdentity($"close-auction-job-{vin}", "auction-jobs")
            .UsingJobData(jobData)
            .Build();
        
        var jobTrigger = TriggerBuilder.Create()
            .WithIdentity($"close-auction-trigger-{vin}", "auction-triggers")
            .StartAt(DateTime.UtcNow.AddMinutes(30))
            .Build();
        
        scheduler.Result.ScheduleJob(job, jobTrigger, cancellationToken);
    }
}