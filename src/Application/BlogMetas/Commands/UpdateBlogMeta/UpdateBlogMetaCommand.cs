using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Wrappers;

using Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogMetas.Commands.UpdateBlogMeta;

public class UpdateBlogMetaCommand : IRequest<Response>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public BlogMetaType Type { get; set; }

    public int Order { get; set; }

    public int Parent { get; set; }
}

public class UpdateBlogMetaCommandHandler : IRequestHandler<UpdateBlogMetaCommand, Response>
{
    private readonly IApplicationDbContext _context;

    public UpdateBlogMetaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(UpdateBlogMetaCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();
        var entity = await _context.BlogMetas
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        if (request.Type != entity.Type)
        {
            var exist = await _context.BlogMetas.AnyAsync(x =>x.Name == name && x.Type == request.Type, cancellationToken);

            if (exist)
            {
                return Response.Failure($"数据[{name}]已存在！");
            }
        }

        if (request.Parent != entity.Parent && request.Parent != 0)
        {
            var parentExist = await _context.BlogMetas.AnyAsync(x => x.Id == request.Parent, cancellationToken);

            if (!parentExist)
            {
                return Response.Failure("父级不存在");
            }
        }

        entity.Name = name;
        entity.Type = request.Type;
        entity.Order = request.Order;
        entity.Description = request.Description;
        entity.Parent = request.Parent;
        
        _context.BlogMetas.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}
