﻿using CleanArchitecture.Application.Features.Orders.Exceptions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Orders.Rules
{
    public class OrderRules
    {
        public Task TheSameUserForTheSameOrder(Guid firstGuid, Guid secondGuid)
        {
            if (!firstGuid.Equals(secondGuid))
                throw new TheSameUserForTheSameOrderException("The Same User should be For The Same Order");
            return Task.CompletedTask;
        }
        public Task TheOrderShouldBeExist(Order? order)
        {
            if (order is null)
                throw new TheOrderShouldBeExistException("this order is not exist in your records");
            return Task.CompletedTask;
        }
    }
}
