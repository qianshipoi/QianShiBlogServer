using Application.Common.Interfaces;
using Application.Common.Wrappers;

using Domain.Entities;
using Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogMetas.Commands.CreateBlogMeta;

public class CreateBlogMetaCommand : IRequest<Response<int>>
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public BlogMetaType Type { get; set; }

    public int Order { get; set; } = 100;

    public int Parent { get; set; }
}

public class CreateBlogMetaCommandHandler : IRequestHandler<CreateBlogMetaCommand, Response<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogMetaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(CreateBlogMetaCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();

        var exist = await _context.BlogMetas
            .AnyAsync(x => x.Name == name && x.Type == request.Type, cancellationToken);

        if (exist)
        {
            return new Response<int>($"元数据[{name}]已存在");
        }

        if (request.Parent != 0)
        {
            var parentExist = await _context.BlogMetas.AnyAsync(x => x.Id == request.Parent, cancellationToken);

            if (!parentExist)
            {
                return Response<int>.Failure("父级不存在");
            }
        }

        var meta = new BlogMeta
        {
            Name = request.Name.Trim(),
            Description = request.Description,
            Order = 100,
            Parent = request.Parent,
            Type = request.Type
        };
        _context.BlogMetas.Add(meta);

        await _context.SaveChangesAsync(cancellationToken);

        return new Response<int>(meta.Id);
    }
}

