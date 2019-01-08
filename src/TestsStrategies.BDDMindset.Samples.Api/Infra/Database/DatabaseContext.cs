using System.Data;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Database
{
	public class DatabaseContext : IDatabaseContext
	{
		public DatabaseContext(IDbConnection connection)
		{
			this.Connection = connection;
			this.Connection.Open();
		}

		public IDbConnection Connection { get; }

		public void Dispose()
		{
			if (this.Connection.State != ConnectionState.Closed)
			{
				this.Connection.Close();
			}
		}
	}
}
