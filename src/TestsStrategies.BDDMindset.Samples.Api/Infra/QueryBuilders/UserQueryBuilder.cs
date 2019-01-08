using System;
using Dapper;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.QueryBuilders
{
	public class UserQueryBuilder
	{
		private readonly DynamicParameters parameters;
		private string query = "";

		public UserQueryBuilder()
		{
			this.parameters = new DynamicParameters();
		}

		public UserQueryBuilder CheckEmail(Email email)
		{
			this.query = @"SELECT 1 FROM Users WHERE Email = @Email";
			this.parameters.Add("Email", email.ToString());
			return this;
		}

		public UserQueryBuilder CreateUser(User user)
		{
			this.query = @"INSERT INTO Users (UserID, Name, Email, PasswordHash, Salt) 
							VALUES(@UserID, @Name, @Email, @PasswordHash, @Salt)";
			this.parameters.Add("UserID", user.Id);
			this.parameters.Add("Name", user.Name.ToString());
			this.parameters.Add("Email", user.Email.ToString());
			this.parameters.Add("PasswordHash", user.Password.HashValue);
			this.parameters.Add("Salt", user.Password.Salt);
			return this;
		}

		public UserQueryBuilder DeleteUser(Guid userdId)
		{
			this.query = @"DELETE FROM Users WHERE UserID = @UserID";
			this.parameters.Add("UserID", userdId);
			return this;
		}

		public UserQueryBuilder GetAll()
		{
			this.query = @"SELECT CAST(UserID as VARCHAR(36)) as Id, Name, Email FROM Users";
			return this;
		}

		public (string, DynamicParameters) Build() =>
			(this.query, this.parameters);
	}
}
