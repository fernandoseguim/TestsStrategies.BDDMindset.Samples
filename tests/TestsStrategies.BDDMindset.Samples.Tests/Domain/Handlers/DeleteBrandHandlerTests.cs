using System;
using FluentAssertions;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Handlers
{
	public partial class BrandHandlerTests : UnitTestBase
    {
	    [Fact]
	    public void Should_contains_bad_request_status_code_in_command_result_when_brand_identifier_is_invalid()
	    {
		    var commandResult = this.brandHandler.Delete(this.MockString());

	        commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.BAD_REQUEST);
	    }

	    [Fact]
	    public void Should_contains_not_found_status_code_in_command_result_when_brand_identifier_not_found_on_database()
	    {
		    var commandResult = this.brandHandler.Delete(this.notFoundBrandId);

	        commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.NOT_FOUND);
	    }

	    [Fact]
	    public void Should_contains_success_status_code_in_command_result_when_brand_identifier_not_found_on_database()
	    {
		    var commandResult = this.brandHandler.Delete(Guid.NewGuid().ToString());

	        commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
	    }
	}
}
