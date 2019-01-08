using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
    public interface IAssetQueryHandler : IQueryHandler
    {
        IQueryResult GetById(string id);
        IQueryResult GetAllByBrandId(string id);
    }
}
