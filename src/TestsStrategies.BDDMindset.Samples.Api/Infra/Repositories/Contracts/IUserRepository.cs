using System.Collections.Generic;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public interface IUserRepository : IEntityRepository<User>
	{
		IEnumerable<UserQueryResult> GetAll();
		bool CheckEmail(Email email);
	}
}
