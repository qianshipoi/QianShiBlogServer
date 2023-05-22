using Application.BlogContents.Queries.GetBlogContentWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogContents.Queries.GetBlogContentWithTextById;

public record GetBlogContentWithTextByIdQuery(int Id) : IRequest<BlogContentWithTextAndMetasDto>;

public class GetBlogContentWithTextByIdQueryHandler : IRequestHandler<GetBlogContentWithTextByIdQuery, BlogContentWithTextAndMetasDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogContentWithTextByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogContentWithTextAndMetasDto> Handle(GetBlogContentWithTextByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents
            .AsNoTracking()
            .Include(x => x.Text)
            .Include(x => x.Metas)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        return _mapper.Map<BlogContentWithTextAndMetasDto>(entity);
    }
}
