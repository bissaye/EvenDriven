using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public sealed class Result<T>
    {
        public bool IsSuccess { get;}
        public T Value { get; }
        public Error Error { get; } 

        private Result(T value) => (IsSuccess, Value, Error) = (true, value, Error.None);
        private Result(Error error) => (IsSuccess, Value, Error) = (false, default!, error);

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(Error error) => new Result<T>(error);
    }

    public record Error(string code, string Message, ErrorType Type)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty, ErrorType.None);
    }

    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        InternalServerError
    }
}
