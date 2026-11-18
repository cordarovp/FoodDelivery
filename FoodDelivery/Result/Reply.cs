
using FoodDelivery.Result.Error;

namespace FoodDelivery.Result
{
    public class Reply
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public ErrorCore Error { get; }

        protected Reply(bool isSuccess, ErrorCore error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Reply Success() => new(true, ErrorCore.None);
        public static Reply Failure(ErrorCore error) => new(false, error);
    }

    public class Reply<T> : Reply
    {
        public T? Value { get; }
        private Reply(T value) : base(true, ErrorCore.None)
        {
            Value = value;
        }

        private Reply(ErrorCore error) : base(false, error)
        {
            Value = default;
        }

        public static Reply<T> Success(T value) => new(value);
        public static new Reply<T> Failure(ErrorCore error)
        {
            return new(error);
        }
    }
}
