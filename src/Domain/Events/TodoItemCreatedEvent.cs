namespace Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }
}
