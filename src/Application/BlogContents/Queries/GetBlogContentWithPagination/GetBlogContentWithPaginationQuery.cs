using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Wrappers;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Enums;

using MediatR;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class GetBlogContentWithPaginationQuery : IRequest<Response<PaginatedList<BlogContentDto>>>
{
    public BlogContentType? Type { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetBlogContentWithPaginationQueryHandler : IRequestHandler<GetBlogContentWithPaginationQuery, Response<PaginatedList<BlogContentDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBlogContentWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<PaginatedList<BlogContentDto>>> Handle(GetBlogContentWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return Response<PaginatedList<BlogContentDto>>.Successful(await _context.BlogContents
         .Where(x => x.Type == request.Type)
         .OrderBy(x => x.Modified)
         .ProjectTo<BlogContentDto>(_mapper.ConfigurationProvider)
         .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken));
    }
}
