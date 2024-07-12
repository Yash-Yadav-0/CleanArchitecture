﻿using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Orders.Exceptions
{
    public class TheOrderShouldBeExistException : BaseException
    {
        public TheOrderShouldBeExistException(string message) : base(message)
        {
        }
    }
}
