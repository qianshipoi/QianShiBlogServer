namespace Application.Common.Wrappers;

public class Response
{
    protected Response()
    {
    }

    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static Response Failure(string? message, IEnumerable<string>? errors = null)
    {
        return new Response
        {
            Succeeded = false,
            Message = message,
            Errors = errors?.ToList()
        };
    }

    public static Response Successful()
    {
        return new Response
        {
            Succeeded = true,
        };
    }
}

public class Response<T> : Response
{
    public Response(T data, string? message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public Response(string? message)
    {
        Succeeded = false;
        Message = message;
    }

    public T? Data { get; set; }

    public static new Response<T> Failure(string? message, IEnumerable<string>? errors = null)
    {
        return new Response<T>(message)
        {
            Errors = errors?.ToList()
        };
    }
    public static Response<T> Successful(T data, string? message = null)
    {
        return new Response<T>(data, message);
    }
}
