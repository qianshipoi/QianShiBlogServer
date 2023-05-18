using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Enums;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogContents.Commands.UpdateBlogContent;

public class UpdateBlogContentCommand : IRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Subtitle { get; set; }

    public int Order { get; set; }

    public BlogContentStatus Status { get; set; }

    public BlogContentType Type { get; set; }

    public string Text { get; set; } = default!;

    public string? HtmlText { get; set; }

    public List<int>? Metas { get; set; }
}

public class UpdateBlogContentCommandHandler : IRequestHandler<UpdateBlogContentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBlogContentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBlogContentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents
            .Include(x => x.Text)
            .Include(x=>x.Metas)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        entity.Title = request.Title;
        entity.Subtitle = request.Subtitle;
        entity.Status = request.Status;
        entity.Type = request.Type;
        entity.AllowComment = request.Type == BlogContentType.Post;
        entity.Order = request.Type == BlogContentType.Post ? 0 : request.Order;
        entity.Text.Text = request.Text;
        entity.Text.HtmlText = request.HtmlText;

        if (request.Metas is not null && request.Metas.Count > 0)
        {
            if (request.Metas.Count == 0)
            {
                foreach (var item in entity.Metas)
                {
                    item.Count--;
                }

                entity.Metas.Clear();
            }
            else
            {
                var oldIds = entity.Metas.Select(x => x.Id);

                var addIds = request.Metas.Except(oldIds);
                var rmIds = oldIds.Except(request.Metas);

                var addItems = await _context.BlogMetas
                    .Where(x => addIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                var rmItems = entity.Metas
                    .Where(x => rmIds.Contains(x.Id))
                    .ToList();


                foreach (var item in rmItems)
                {
                    item.Count--;
                    entity.Metas.Remove(item);
                }

                foreach (var item in addItems)
                {
                    item.Count++;
                    entity.Metas.Add(item);
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
