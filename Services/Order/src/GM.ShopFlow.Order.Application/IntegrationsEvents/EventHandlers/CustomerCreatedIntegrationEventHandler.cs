using EventBus.Abstractions;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Order.Application.Interfaces;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;

public class CustomerCreatedIntegrationEventHandler(IUserService userService) : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
{
    private readonly IUserService _userService = userService;

    public async Task HandleAsync(CustomerCreatedIntegrationEvent @event)
    {
        await _userService.AddRoleCustomerToUserAsync(@event.UserId);
    }
}
