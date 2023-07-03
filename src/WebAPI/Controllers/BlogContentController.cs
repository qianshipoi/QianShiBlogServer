using Application.BlogContents.Commands.CreateBlogContent;
using Application.BlogContents.Commands.DeleteBlogContent;
using Application.BlogContents.Commands.PublishBlogContent;
using Application.BlogContents.Commands.UpdateBlogContent;
using Application.BlogContents.Queries.GetBlogContentById;
using Application.BlogContents.Queries.GetBlogContentWithPagination;
using Application.BlogContents.Queries.GetBlogContentWithTextById;
using Application.Common.Models;
using Application.Common.Wrappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class BlogContentController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<Response<BlogContentDto>> GetById(int id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new GetBlogContentByIdQuery(id), cancellationToken);

    [HttpGet("{id:int}/detail")]
    public async Task<Response<BlogContentWithTextAndMetasDto>> GetWithTextById(int id, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(new GetBlogContentWithTextByIdQuery(id), cancellationToken);
    }

    [HttpGet]
    public async Task<Response<PaginatedList<BlogContentDto>>> GetBlogContents([FromQuery] GetBlogContentWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task Create(CreateBlogContentCommand command, CancellationToken cancellationToken = default)
        => await Mediator.Send(command, cancellationToken);

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateBlogContentCommand command, CancellationToken cancellationToken = default)
    {
        command.Id = id;
        await Mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new DeleteBlogContentCommand(id), cancellationToken);

    [HttpPut("{id:int}/publish")]
    public async Task Publish(int id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new PublishBlogContentCommand(id, true), cancellationToken);

    [HttpPut("{id:int}/unpublish")]
    public async Task Unpublish(int id, CancellationToken cancellationToken = default)
        => await Mediator.Send(new PublishBlogContentCommand(id, false), cancellationToken);
}
