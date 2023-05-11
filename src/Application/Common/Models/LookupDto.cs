using Application.Common.Mappings;

using Domain.Entities;

namespace Application.Common.Models;

public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}