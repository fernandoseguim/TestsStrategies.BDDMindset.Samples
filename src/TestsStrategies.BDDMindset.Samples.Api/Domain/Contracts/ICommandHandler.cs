using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts
{
	public interface ICommandHandler<in TCommand> where TCommand : BaseCommand
	{
		ICommandResult Create(TCommand command);
		ICommandResult Update(string id, TCommand command);
		ICommandResult Delete(string id);
	}
}
