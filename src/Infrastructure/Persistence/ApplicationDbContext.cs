using Application.Common.Interfaces;

using Domain.Entities;

using Infrastructure.Common;
using Infrastructure.Persistence.Interceptors;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(DbContextOptions options, IMediator mediator, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<UserInfo> UserInfos => Set<UserInfo>();

    public DbSet<Attachment> Attachments => Set<Attachment>();

    public DbSet<BlogContent> BlogContents => Set<BlogContent>();

    public DbSet<BlogContentText> BlogContentTexts => Set<BlogContentText>();

    public DbSet<BlogMeta> BlogMetas => Set<BlogMeta>();

    public DbSet<BlogRelationships> BlogRelationships => Set<BlogRelationships>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<BlogContent>()
            .HasOne(b => b.Text)
            .WithOne(t => t.Content)
            .HasForeignKey<BlogContentText>(t => t.Id);

        builder.Entity<BlogRelationships>()
            .HasOne(b => b.Content)
            .WithMany(t => t.Relationships)
            .HasForeignKey(b => b.Cid);

        builder.Entity<BlogRelationships>()
            .HasOne(b => b.Meta)
            .WithMany(t => t.Relationships)
            .HasForeignKey(b => b.Mid);

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}