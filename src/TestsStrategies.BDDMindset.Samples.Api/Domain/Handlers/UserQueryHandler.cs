using System;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
    public class UserQueryHandler : IUserQueryHandler
	{
		private readonly IUserRepository repository;

		public UserQueryHandler(IUserRepository repository) => this.repository = repository;


		public IQueryResult GetAll()
		{
			try
			{
				var result = this.repository.GetAll();

				return new SuccessfulQueryResult(result);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulQueryResult(StatusCodeResult.INTERNAL_SERVER_ERROR,ex.Message);
			}
		}
	}
}
