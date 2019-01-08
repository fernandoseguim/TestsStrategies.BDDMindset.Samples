using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Flunt.Notifications;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Handlers
{
	public partial class BrandHandlerTests : UnitTestBase
	{
		[Fact]
		public void Should_contains_conflict_status_code_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(this.Fixture.Create<Guid>().ToString(), this.brandCommand);

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.CONFLICT);
		}

		[Fact]
		public void Should_contains_notification_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(Guid.NewGuid().ToString(), this.brandCommand);

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.First().Property.Should().Be("Name");
		}

		[Fact]
		public void Should_contains_not_found_status_code_in_command_result_when_brand_name_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Update(this.notFoundBrandId, new BrandCommand{ Name = this.MockString() });

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.NOT_FOUND);
		}

		[Fact]
		public void Should_contains_notification_in_command_result_when_brand_not_found_in_database()
		{
			var commandResult = this.brandHandler.Update(this.notFoundBrandId, new BrandCommand { Name = this.MockString() });

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.First().Property.Should().Be("Id");
		}

		[Fact]
		public void Should_contains_bad_request_status_code_in_command_result_when_trying_update_brand_with_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.BAD_REQUEST);
		}

		[Fact]
		public void Should_contains_notifications_command_result_trying_update_brand_with_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.Should().NotBeEmpty();
		}

		[Fact]
		public void Should_contains_success_status_code_in_command_result_when_trying_update_user_with_comand_is_valid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand() { Name = this.MockString() });

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
        }
	}
}
