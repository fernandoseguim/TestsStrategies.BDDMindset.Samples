using Newtonsoft.Json;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response
{
	public class SuccessfulQueryResult : IQueryResult
	{
		public SuccessfulQueryResult(object data)
		{
			this.Data = data;
		}

		[JsonIgnore]
		public StatusCodeResult StatusCode => StatusCodeResult.SUCCESS;
		public bool Success => true;
		public object Data { get; }
	}
}
