using System;
using Flunt.Notifications;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity() => this.Id = Guid.NewGuid();

        public Guid Id { get; protected set; }
    }
}
