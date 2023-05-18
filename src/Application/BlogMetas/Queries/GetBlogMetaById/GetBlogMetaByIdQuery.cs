using Application.BlogMetas.Queries.GetBlogMetasWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogMetas.Queries.GetBlogMetaById;

public record GetBlogMetaByIdQuery(int Id) : IRequest<BlogMetaDto>;

public record GetBlogMetaByIdQueryHandler : IRequestHandler<GetBlogMetaByIdQuery, BlogMetaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBlogMetaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogMetaDto> Handle(GetBlogMetaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogMetas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogMeta), request.Id);
        }

        return _mapper.Map<BlogMetaDto>(entity);
    }
}
