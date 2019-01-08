using System;
using System.Collections.Generic;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public interface IBrandRepository : IEntityRepository<Brand>
	{
		IEnumerable<BrandQueryResult> GetAll();
		BrandQueryResult GetById(Guid brandId);
		bool CheckBrand(string name);
		bool Update(Guid id, BrandCommand brandCommand);
	}
}
