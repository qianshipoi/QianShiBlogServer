using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

namespace Application.BlogContents.Commands.PublishBlogContent;

public record PublishBlogContentCommand(int Id, bool IsPublish) : IRequest;

public class PublishBlogContentCommandHandler : IRequestHandler<PublishBlogContentCommand>
{
    private readonly IApplicationDbContext _context;

    public PublishBlogContentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PublishBlogContentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents.FindAsync(new object[] { request.Id }, cancellationToken);

        if(entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        entity.Status = request.IsPublish ? Domain.Enums.BlogContentStatus.Publish : Domain.Enums.BlogContentStatus.Draft;

        await _context.SaveChangesAsync(cancellationToken);
    }

}
