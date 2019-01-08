using Flunt.Notifications;
using System;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
    public partial class UserHandler : Notifiable, ICommandHandler<UserCommand>
    {
        public ICommandResult Update(string id, UserCommand command)
        {
            //TODO: Implementar durante a demo
            throw new NotImplementedException();
        }
    }
}
