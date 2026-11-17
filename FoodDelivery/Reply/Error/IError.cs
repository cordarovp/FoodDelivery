namespace FoodDelivery.Reply.Error
{
    public interface IError
    {
        string Title { get; }
        string Message { get; }
        int StatusCode { get; }
        object? Payload { get; }
    }
}
