using System;
using FluentAssertions;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Entities
{
	public class BrandTests : UnitTestBase
	{
		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_brand_with_null_name()
		{
		    WhenInstance<Brand>(() => new Brand(null))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_brand_with_withspace_name()
		{
		    WhenInstance<Brand>(() => new Brand(string.Empty))
		       .Should()
		       .Throw<ArgumentNullException>();
		}
	}
}
