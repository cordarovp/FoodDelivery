namespace FoodDelivery.Reply.Error
{
    internal sealed record ErrorCore : IError
    {
        public string Title { get; }
        public string Message { get; }
        public int StatusCode { get; }
        public object? Payload { get; }

        private ErrorCore(string title, string message, int statusCode, object? payload) 
        {
            Title = title;
            Message = message;
            StatusCode = statusCode;
            Payload = payload;
        }

        internal static IError ResourceNotFoundError(string message, object? payload = null) => new ErrorCore("ResourceNotFoundError", message, 404, payload);
        internal static IError ValidationError(string message, object? payload = null) => new ErrorCore("ValidationError", message, 400, payload);
    }
}
