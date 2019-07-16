using System;
using Amaury.Abstractions;
using Amaury.Attributes;
using Amaury.Sample.MediatR.Domain.Entities;

namespace Amaury.Sample.MediatR.Domain.Events
{
    [EventName("CUSTOMER_WAS_CREATED")]
    public class CustomerWasCreated : CelebrityEvent<Customer>
    {
        public CustomerWasCreated(Guid aggregatedId, Customer data) : base(aggregatedId, data) { }

    }
}
