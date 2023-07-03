using Application.BlogMetas.Queries.GetBlogMetasWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Wrappers;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogMetas.Queries.GetBlogMetaById;

public record GetBlogMetaByIdQuery(int Id) : IRequest<Response<BlogMetaDto>>;

public record GetBlogMetaByIdQueryHandler : IRequestHandler<GetBlogMetaByIdQuery, Response<BlogMetaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBlogMetaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<BlogMetaDto>> Handle(GetBlogMetaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogMetas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogMeta), request.Id);
        }

        return Response<BlogMetaDto>.Successful(_mapper.Map<BlogMetaDto>(entity));
    }
}
