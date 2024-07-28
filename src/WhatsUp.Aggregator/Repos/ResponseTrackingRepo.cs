using FluentResults;
using Microsoft.EntityFrameworkCore;
using WhatsUp.Aggregator.Entities;

namespace WhatsUp.Aggregator.Repos;

public class ResponseTrackingRepo(WhatsUpDbContext context) : IResponseTrackingRepo
{
    public async Task<Result> AddEntityWithRuleAsync(ResponseTrackerEntry newEntity)
    {
        try
        {
            var entitiesWithSpecificProperty = await context.Entries
                .Where(e => e.RequestPath == newEntity.RequestPath)
                .OrderBy(e => e.Id)
                .ToListAsync();

            var excessCount = entitiesWithSpecificProperty.Count - 49;

            if (excessCount > 0)
            {
                var entitiesToRemove = entitiesWithSpecificProperty.Take(excessCount).ToList();
                context.Entries.RemoveRange(entitiesToRemove);
            }

            context.Entries.Add(newEntity);
            await context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ex.Message));
        }
    }

    public async Task<Result<IReadOnlyCollection<ResponseTrackerEntry>>> GetEntriesForRouteAsync(string route)
    {
        try
        {
            var entities = await context.Entries
                .Where(e => e.RequestPath == route)
                .OrderBy(e => e.Id)
                .ToListAsync();

            return Result.Ok<IReadOnlyCollection<ResponseTrackerEntry>>(entities);
        }
        catch (Exception e)
        {
            return Result.Fail<IReadOnlyCollection<ResponseTrackerEntry>>(new Error(e.Message));
        }
    }
}