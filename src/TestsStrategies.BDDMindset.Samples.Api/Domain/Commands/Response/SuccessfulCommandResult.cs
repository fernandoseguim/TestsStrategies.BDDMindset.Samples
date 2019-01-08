using Newtonsoft.Json;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response
{
	public class SuccessfulCommandResult : ICommandResult
	{
		public SuccessfulCommandResult(string message, object data)
		{
			this.StatusCode = StatusCodeResult.SUCCESS;
			this.Success = true;
			this.Message = message;
			this.Data = data;
		}

		[JsonIgnore]
		public StatusCodeResult StatusCode { get; }
		public bool Success { get; }
		public string Message { get; }
		public object Data { get; }
	}
}
