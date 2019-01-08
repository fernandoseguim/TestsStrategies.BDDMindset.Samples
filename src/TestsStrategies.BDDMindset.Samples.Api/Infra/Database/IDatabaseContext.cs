using System;
using System.Data;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Database
{
	public interface IDatabaseContext : IDisposable
	{
		IDbConnection Connection { get; }
	}
}