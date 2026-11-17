using FoodDelivery.Reply.Error;

namespace FoodDelivery.Reply
{
    public class Reply
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IError? Error { get; }

        protected Reply(bool isSuccess, IError? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Reply Success() => new(true, null);
        public static Reply Failure(IError error)
        {
            return new(false, error);
        }

        public static Reply NotFoundError(string message, object? payload = null)
        {
            var error = ErrorCore.ResourceNotFoundError(message, payload);
            return new(false, error);
        }

        public static Reply ValidationError(string message, object? payload = null)
        {
            var error = ErrorCore.ValidationError(message, payload);
            return new(false, error);
        }
    }

    public sealed class Reply<T> : Reply
    {
        public T? Value;
        private Reply(T value) : base(true, null)
        {
            Value = value;
        }

        private Reply(IError error) : base(false, error)
        {
            Value = default;
        }

        public static Reply<T> Success(T value) => new(value);
        public static new Reply<T> Failure(IError error)
        {
            return new(error);
        }

        public static new Reply<T> NotFoundError(string message, object? payload = null)
        {
            var error = ErrorCore.ResourceNotFoundError(message, payload);
            return new(error);
        }

        public static new Reply<T> ValidationError(string message, object? payload = null)
        {
            var error = ErrorCore.ValidationError(message, payload);
            return new(error);
        }
    }
}
