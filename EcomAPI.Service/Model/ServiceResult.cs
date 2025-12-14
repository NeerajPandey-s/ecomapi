using System;

namespace EcomAPI.Service.Data
{
    public readonly struct ServiceResult<TValue, TError>
    {
        public bool IsError { get; }
        public bool IsSuccess => !IsError;

        private readonly TValue? _value;
        private readonly TError? _error;

        public ServiceResult(TValue value)
        {
            IsError = false;
            _value = value;
            _error = default;
        }

        public ServiceResult(TError error)
        {
            IsError = true;
            _error = error;
            _value = default;
        }

        public static implicit operator ServiceResult<TValue, TError>(TValue value) => new(value);

        public static implicit operator ServiceResult<TValue, TError>(TError error) => new(error);

        public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure) =>
            !IsError ? success(_value!) : failure(_error!);
    }
}
