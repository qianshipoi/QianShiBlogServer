using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogContents.Commands.CreateBlogContent;

public class CreateBlogContentCommand : IRequest
{
    public string Title { get; set; } = default!;

    public string? Subtitle { get; set; }

    public int Order { get; set; }

    public BlogContentStatus Status { get; set; }

    public BlogContentType Type { get; set; }

    public string Text { get; set; } = default!;

    public string? HtmlText { get; set; }

    public List<int>? Metas { get; set; }
}

public class CreateBlogContentCommandHandler : IRequestHandler<CreateBlogContentCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogContentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBlogContentCommand request, CancellationToken cancellationToken)
    {
        var entity = new BlogContent
        {
            Title = request.Title,
            Subtitle = request.Subtitle,
            Status = request.Status,
            Type = request.Type,
            Text = new BlogContentText
            {
                Text = request.Text,
                HtmlText = request.HtmlText
            },
            AllowComment = request.Type == BlogContentType.Post
        };

        if (request.Metas is not null && request.Metas.Count > 0)
        {
            var metas = await _context.BlogMetas.Where(x => request.Metas.Distinct().Contains(x.Id)).ToListAsync();

            foreach (var meta in metas)
            {
                meta.Count++;
                entity.Metas.Add(meta);
            }
        }

        await _context.BlogContents.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
