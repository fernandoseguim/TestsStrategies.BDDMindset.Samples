using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response
{
	public class UnsuccessfulQueryResult : IQueryResult
	{
		public UnsuccessfulQueryResult(StatusCodeResult statusCode, object data)
		{
			this.StatusCode = statusCode;
			this.Data = data;
		}

		public StatusCodeResult StatusCode { get; }
		public bool Success => false;
		public object Data { get; }
	}
}
