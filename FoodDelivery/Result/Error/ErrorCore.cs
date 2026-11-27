namespace FoodDelivery.Result.Error
{
    public sealed record ErrorCore(string Message, ErrorType Type)
    {
        public static readonly ErrorCore None = new(string.Empty, ErrorType.None);

        public static ErrorCore NotFound(string message) => new(message, ErrorType.NotFound);
        public static ErrorCore Validation(string message) => new(message, ErrorType.Validation);
        public static ErrorCore Conflict(string message) => new(message, ErrorType.Conflict);
        public static ErrorCore UnprocessableContent(string message) => new(message, ErrorType.Unprocessable);
        public static ErrorCore Unauthorized(string message) => new(message, ErrorType.Unauthorized);
        public static ErrorCore Forbidden(string message) => new(message, ErrorType.Forbidden);
    }
}
