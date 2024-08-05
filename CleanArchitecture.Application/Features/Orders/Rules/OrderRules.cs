using CleanArchitecture.Application.Features.Orders.Exceptions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Orders.Rules
{
    public interface IOrderRules
    {
        Task TheSameUserForTheSameOrder(Guid currentUserID, Guid orderUserID);
        Task TheOrderShouldBeExist(Order? order);
    }
    public class OrderRules : IOrderRules
    {
        public Task TheSameUserForTheSameOrder(Guid currentUserId, Guid orderUserId)
        {
            if (!currentUserId.Equals(orderUserId))
                throw new TheSameUserForTheSameOrderException("The Same User should be For The Same Order");
            return Task.CompletedTask;
        }

        public Task TheOrderShouldBeExist(Order? order)
        {
            if (order is null)
                throw new TheOrderShouldBeExistException("This order does not exist in your records");
            return Task.CompletedTask;
        }
    }
}
