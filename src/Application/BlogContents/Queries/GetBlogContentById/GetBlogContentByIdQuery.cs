using Application.BlogContents.Queries.GetBlogContentWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogContents.Queries.GetBlogContentById;

public record GetBlogContentByIdQuery(int Id) : IRequest<BlogContentDto>;

public class GetBlogContentByIdQueryHandler : IRequestHandler<GetBlogContentByIdQuery, BlogContentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogContentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogContentDto> Handle(GetBlogContentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents
            .AsNoTracking()
            .Include(x => x.Text)
            //.Include(x => x.Relationships)
            //.ThenInclude(x => x.Meta)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        return _mapper.Map<BlogContentDto>(entity);
    }
}
