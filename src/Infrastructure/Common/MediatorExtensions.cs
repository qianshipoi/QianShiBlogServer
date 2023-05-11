using Domain.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity);

        var domainEvents = entities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvent());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
