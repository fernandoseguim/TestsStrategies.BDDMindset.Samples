using System;
using System.Collections.Generic;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public interface IAssetRepository : IEntityRepository<Asset>
	{
		IEnumerable<AssetQueryResult> GetAll();
		AssetQueryResult GetById(Guid assetId);
		IEnumerable<AssetQueryResult> GetAllByBrandId(Guid brandId);
		bool Update(Guid id, AssetCommand assetCommand);
	} 
}

