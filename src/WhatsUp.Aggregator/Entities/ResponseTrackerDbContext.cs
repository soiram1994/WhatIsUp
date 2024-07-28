using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace WhatsUp.Aggregator.Entities;

public class WhatsUpDbContext : DbContext
{
    public DbSet<ResponseTrackerEntry> Entries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("WhatsUpDb");
    }
}

public interface IResponseTrackingRepo
{
    Task<Result> AddEntityWithRuleAsync(ResponseTrackerEntry newEntity);
    Task<Result<IReadOnlyCollection<ResponseTrackerEntry>>> GetEntriesForRouteAsync(string route);
}