using Application.Common.Interfaces;

using Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public AuditableEntitySaveChangesInterceptor(IDateTime dateTime, ICurrentUserService currentUserService)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entity in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedBy = _currentUserService.UserId;
                entity.Entity.Created = _dateTime.Now;
            }

            if (entity.State == EntityState.Added || entity.State == EntityState.Modified || entity.HasChangedOwnedEntities())
            {
                entity.Entity.Modified = _dateTime.Now;
            }
        }

    }
}

public static class Extenctions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entity) =>
        entity.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
