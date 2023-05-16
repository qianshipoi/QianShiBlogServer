using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<UserInfo> UserInfos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
