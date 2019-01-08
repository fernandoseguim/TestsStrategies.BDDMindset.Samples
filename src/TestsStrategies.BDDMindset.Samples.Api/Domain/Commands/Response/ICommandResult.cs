using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response
{
	public interface ICommandResult
	{
		StatusCodeResult StatusCode { get; }
		bool Success { get; }
		string Message { get; }
		object Data { get; }
	}
}
