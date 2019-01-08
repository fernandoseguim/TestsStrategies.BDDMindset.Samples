using System;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Factories
{
	public class BrandFactory
	{
		public static Brand Create(BrandCommand command) => new Brand(command.Name);
		public static Brand Create(BrandQueryResult queryResult) => new Brand(new Guid(queryResult.Id), queryResult.Name);
	}
}