using System;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public interface IEntityRepository<in TEntity> where TEntity : Entity
	{
		void Save(TEntity entity);
		bool Delete(Guid id);
	}
}
