using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Wrappers;

using Domain.Entities;

using Domain.Events;

using MediatR;

namespace Application.BlogMetas.Commands.DeleteBlogMeta;

public record DeleteBlogMetaCommand(int Id) : IRequest;

public class DeleteBlogMetaCommandHandler : IRequestHandler<DeleteBlogMetaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBlogMetaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBlogMetaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogMetas.FindAsync(new object[] { request.Id }, cancellationToken);

        if(entity == null)
        {
            throw new NotFoundException(nameof(BlogMeta), request.Id);
        }

        _context.BlogMetas.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
