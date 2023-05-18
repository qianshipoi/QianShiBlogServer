using Application.Common.Interfaces;

using Domain.Events;

using MediatR;

namespace Application.BlogContents.EventHandlers;

public class BlogContentDeletedEventHandler : INotificationHandler<BlogContentDeletedEvent>
{
    private readonly IApplicationDbContext _context;

    public BlogContentDeletedEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(BlogContentDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var item in notification.Item.Metas)
        {
            item.Count--;
        }

        notification.Item.Metas.Clear();

        await _context.SaveChangesAsync(cancellationToken);
    }
}
