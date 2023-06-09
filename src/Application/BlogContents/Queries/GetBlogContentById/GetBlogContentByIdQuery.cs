﻿using Application.BlogContents.Queries.GetBlogContentWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Wrappers;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.BlogContents.Queries.GetBlogContentById;

public record GetBlogContentByIdQuery(int Id) : IRequest<Response<BlogContentDto>>;

public class GetBlogContentByIdQueryHandler : IRequestHandler<GetBlogContentByIdQuery, Response<BlogContentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogContentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<BlogContentDto>> Handle(GetBlogContentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BlogContents
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(BlogContent), request.Id);
        }

        return Response<BlogContentDto>.Successful(_mapper.Map<BlogContentDto>(entity));
    }
}
