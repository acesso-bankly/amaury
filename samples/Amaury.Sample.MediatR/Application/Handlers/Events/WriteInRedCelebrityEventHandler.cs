using System;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions.Handlers;
using Amaury.Sample.MediatR.Domain.Entities;
using Amaury.Sample.MediatR.Domain.Events;

namespace Amaury.Sample.MediatR.Application.Handlers.Events
{
    public class WriteInRedCelebrityEventHandler : ICelebrityEventHandler<Customer, CustomerWasCreated>
    {
        public async Task Handle(CustomerWasCreated notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine($"Event Name: {notification.Name}");

            }, cancellationToken);
        }
    }

    public class WriteInGreenCelebrityEventHandler : ICelebrityEventHandler<Customer, CustomerWasCreated>
    {
        public async Task Handle(CustomerWasCreated notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Event Name: {notification.Name}");

            }, cancellationToken);
        }
    }
}
