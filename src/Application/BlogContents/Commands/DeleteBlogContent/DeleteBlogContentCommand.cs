using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Events;

using MediatR;

namespace Application.BlogContents.Commands.DeleteBlogContent;

public record DeleteBlogContentCommand(int Id) : IRequest;

public class DeleteBlogContentCommandHandler : IRequestHandler<DeleteBlogContentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBlogContentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBlogContentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        entity.AddDomainEvent(new BlogContentDeletedEvent(entity));

        entity.Status = Domain.Enums.BlogContentStatus.Deleted;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
