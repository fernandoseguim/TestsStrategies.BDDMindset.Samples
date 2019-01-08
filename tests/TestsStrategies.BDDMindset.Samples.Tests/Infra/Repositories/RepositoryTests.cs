using System;
using System.Linq;
using NSubstitute;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Database;

namespace TestsStrategies.BDDMindset.Samples.Tests.Infra.Repositories
{
	public abstract class RepositoryTests<T> where T : class
	{
		protected RepositoryTests()
		{
			var fakeDatabase = new InMemoryDatabase();
			fakeDatabase.CreateTable<T>();

			this.Context = Substitute.For<IDatabaseContext>();
			this.Context.Connection.Returns(fakeDatabase.OpenConnection());
		}

	    protected IDatabaseContext Context { get; }

		protected string MockString(int length = 10)
		{
			var random = new Random();
			const string CHARS = "0123456789";
			return new string(Enumerable.Repeat(CHARS, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}