using Application.BlogMetas.Commands.CreateBlogMeta;
using Application.BlogMetas.Commands.DeleteBlogMeta;
using Application.BlogMetas.Commands.UpdateBlogMeta;
using Application.BlogMetas.Queries.GetBlogMetaById;
using Application.BlogMetas.Queries.GetBlogMetasWithPagination;
using Application.Common.Models;
using Application.Common.Wrappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class BlogMetaController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<Response<BlogMetaDto>> GetBlogMetaById(int id, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(new GetBlogMetaByIdQuery(id), cancellationToken);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<Response<PaginatedList<BlogMetaDto>>> GetBlogMetas([FromQuery] GetBlogMetasWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Response<int>> Create(CreateBlogMetaCommand command, CancellationToken cancellationToken = default)
        => await Mediator.Send(command, cancellationToken);

    [HttpPut("{id:int}")]
    public async Task<Response> Update(int id, UpdateBlogMetaCommand command, CancellationToken cancellationToken = default)
    {
        command.Id = id;
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new DeleteBlogMetaCommand(id), cancellationToken);
}
