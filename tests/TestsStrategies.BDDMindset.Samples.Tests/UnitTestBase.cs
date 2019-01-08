using System;
using System.Linq;
using AutoFixture;

namespace TestsStrategies.BDDMindset.Samples.Tests
{
	public abstract class UnitTestBase
	{
	    protected Fixture Fixture => new Fixture();

	    protected static Action WhenInstance<T>(Func<T> func) => () => func();

        protected string MockString(int length = 10)
		{
			var random = new Random();
			const string CHARS = "0123456789";
			return new string(Enumerable.Repeat(CHARS, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
