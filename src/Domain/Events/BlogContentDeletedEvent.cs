namespace Domain.Events;

public class BlogContentDeletedEvent : BaseEvent
{
    public BlogContentDeletedEvent(BlogContent item)
    {
        Item = item;
    }

    public BlogContent Item { get; }
}
