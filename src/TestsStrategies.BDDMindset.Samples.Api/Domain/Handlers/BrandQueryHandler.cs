using System;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
    public class BrandQueryHandler : IBrandQueryHandler
	{
		private readonly IBrandRepository repository;

		public BrandQueryHandler(IBrandRepository repository) => this.repository = repository;


		public IQueryResult GetAll()
		{
			try
			{
				var result = this.repository.GetAll();

				return new SuccessfulQueryResult(result);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}

		public IQueryResult GetById(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var brandId))
				{
					var brand = this.repository.GetById(brandId);
					if (brand is null)
					{
						return new UnsuccessfulQueryResult(StatusCodeResult.NOT_FOUND,
							$"The brand {brandId} not found");
					}

					return new SuccessfulQueryResult(brand);
				}

				return new UnsuccessfulQueryResult(StatusCodeResult.BAD_REQUEST,
					"The brand identifier should be a valid GUID");
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.INTERNAL_SERVER_ERROR,
					"There was an error saving the user, please contact your system administrator.");
			}
		}
	}
}
