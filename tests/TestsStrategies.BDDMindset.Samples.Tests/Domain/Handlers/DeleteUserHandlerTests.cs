using System;
using AutoFixture;
using FluentAssertions;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Handlers
{
	public partial class UserHandlerTests : UnitTestBase
	{
		[Fact]
		public void Should_return_bad_request_status_result_when_user_identifier_is_invalid()
		{
			var commandResult = this.userHandler.Delete(this.MockString());

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.BAD_REQUEST);
		}
		
		[Fact]
		public void Should_return_not_found_status_code_result_when_user_identifier_not_found_on_database()
		{
		    var commandResult = this.userHandler.Delete(this.notFoundUserId);

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.NOT_FOUND);
		}

		[Fact]
		public void Should_return_success_status_code_result_when_user_identifier_not_found_on_database()
		{
			var commandResult = this.userHandler.Delete(Guid.NewGuid().ToString());

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
		}
	}
}
