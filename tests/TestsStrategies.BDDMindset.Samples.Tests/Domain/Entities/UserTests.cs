using System;
using FluentAssertions;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Entities
{
	public class UserTests : UnitTestBase
	{
		private readonly Name name;
		private readonly Email email;
		private readonly SecurityPassword password;

		public UserTests()
		{
			this.name = new Name("Fernando", "Seguim");
			this.email = new Email("fernando.seguim@gmail.com");
			this.password = new SecurityPassword(this.MockString());
		}

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_name()
		{
		    WhenInstance<User>(() => new User(null, this.email, this.password))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_email()
		{
		    WhenInstance<User>(() => new User(this.name, null, this.password))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_user_with_null_password()
		{
		    WhenInstance<User>(() => new User(this.name, this.email, null))
		       .Should()
		       .Throw<ArgumentNullException>();
		}
	}
}
