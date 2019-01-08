using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
	public interface IBrandQueryHandler : IQueryHandler
	{
		IQueryResult GetById(string id);
	}
}
