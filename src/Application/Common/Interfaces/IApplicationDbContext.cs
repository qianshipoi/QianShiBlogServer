using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<UserInfo> UserInfos { get; }

    DbSet<Attachment> Attachments { get; }

    DbSet<BlogContent> BlogContents { get; }

    DbSet<BlogContentText> BlogContentTexts { get; }

    DbSet<BlogMeta> BlogMetas { get; }

    DbSet<BlogRelationships> BlogRelationships { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
