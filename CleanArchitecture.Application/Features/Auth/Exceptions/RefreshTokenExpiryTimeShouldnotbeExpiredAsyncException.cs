﻿using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException : BaseException
    {
        public RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException(string message) : base(message) { }
        public RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException() : base() { }
    }
}
