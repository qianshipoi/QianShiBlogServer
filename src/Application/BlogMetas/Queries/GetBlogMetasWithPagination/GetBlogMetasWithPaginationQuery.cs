using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Enums;

using MediatR;

namespace Application.BlogMetas.Queries.GetBlogMetasWithPagination;

public class GetBlogMetasWithPaginationQuery : IRequest<PaginatedList<BlogMetaDto>>
{
    public BlogMetaType? Type { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}

public class GetBlogMetaWithPaginationQueryHandler : IRequestHandler<GetBlogMetasWithPaginationQuery, PaginatedList<BlogMetaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogMetaWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BlogMetaDto>> Handle(GetBlogMetasWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.BlogMetas
           .Where(x => x.Type == request.Type)
           .OrderBy(x => x.Order)
           .ThenBy(x => x.Created)
           .ProjectTo<BlogMetaDto>(_mapper.ConfigurationProvider)
           .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
