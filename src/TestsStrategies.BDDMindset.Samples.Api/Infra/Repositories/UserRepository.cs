using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Database;
using TestsStrategies.BDDMindset.Samples.Api.Infra.QueryBuilders;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IDatabaseContext context;

		public UserRepository(IDatabaseContext context) => this.context = context;

		public IEnumerable<UserQueryResult> GetAll()
		{
			var (query, parameters) = new UserQueryBuilder().GetAll().Build();

			var users = this.context.Connection.Query<UserQueryResult>(query);

			return users;
		}

		public void Save(User user)
		{
			var (query, parameters) = new UserQueryBuilder().CreateUser(user).Build();

			this.context.Connection.Execute(query, parameters);
		}

		public bool CheckEmail(Email email)
		{
			var (query, parameters) = new UserQueryBuilder().CheckEmail(email).Build();

			var result = this.context.Connection.Query<bool>(query, parameters).FirstOrDefault();
			return result;
		}

		public bool Delete(Guid userId)
		{
			var (query, parameters) = new UserQueryBuilder().DeleteUser(userId).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}
	}
}
