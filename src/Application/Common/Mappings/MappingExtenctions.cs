using Application.Common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Mappings;

public static class MappingExtenctions
{
    public static Task<PaginatedList<TDescription>> PaginatedListAsync<TDescription>(this IQueryable<TDescription> queryable, int pageNumber, int pageSize)
        => PaginatedList<TDescription>.CreateAsync(queryable, pageNumber, pageSize);

    public static Task<List<TDescription>> ProjectToListAsync<TDescription>(this IQueryable queryable, IConfigurationProvider configuration) where TDescription : class
        => queryable.ProjectTo<TDescription>(configuration).AsNoTracking().ToListAsync();
}

