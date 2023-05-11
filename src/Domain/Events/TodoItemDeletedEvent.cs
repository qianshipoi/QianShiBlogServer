namespace Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }
}