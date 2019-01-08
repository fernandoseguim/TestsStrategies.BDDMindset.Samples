using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts
{
	public interface IQueryResult
    {
	    StatusCodeResult StatusCode { get; }
	    bool Success { get; }
	    object Data { get; }
	}
}
