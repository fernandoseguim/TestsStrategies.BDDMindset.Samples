using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Flunt.Notifications;
using NSubstitute;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Handlers
{
	public partial class BrandHandlerTests : UnitTestBase
	{
	    private readonly string notFoundBrandId;
		private readonly IBrandRepository repository;
		private readonly ICommandHandler<BrandCommand> brandHandler;
		private readonly BrandCommand brandCommand;

		public BrandHandlerTests()
		{
            this.notFoundBrandId = this.Fixture.Create<Guid>().ToString();

            this.brandCommand = new BrandCommand();
			this.repository = Substitute.For<IBrandRepository>();
			this.brandHandler = new BrandHandler(this.repository);

		    this.brandCommand.Name = this.MockString();
		    this.repository.CheckBrand(Arg.Is<string>(name => name.Equals(this.brandCommand.Name))).Returns(true);
		    this.repository.CheckBrand(Arg.Is<string>(name => !name.Equals(this.brandCommand.Name))).Returns(false);
		    this.repository.Delete(Arg.Any<Guid>()).Returns(true);
		    this.repository.Delete(Arg.Is<Guid>(id => id.Equals(new Guid(this.notFoundBrandId)))).Returns(false);

		    this.repository.Update(Arg.Any<Guid>(), Arg.Any<BrandCommand>()).Returns(true);
		    this.repository.Update(Arg.Is<Guid>(id => id.Equals(new Guid(this.notFoundBrandId))), Arg.Any<BrandCommand>()).Returns(false);

        }

		[Fact]
		public void Should_contains_conflict_status_code_in_command_result_when_email_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Create(this.brandCommand);

            commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.CONFLICT);
		}

		[Fact]
		public void Should_contains_notification_in_command_result_when_brand_already_exists_in_database()
		{
			var commandResult = this.brandHandler.Create(this.brandCommand);

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.First().Property.Should().Be("Name");
		}

		[Fact]
		public void Should_contains_notifications_command_result_when_brand_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.Should().NotBeEmpty();
		}

		[Fact]
		public void Should_contains_bad_request_status_code_in_command_result_when_brand_comand_is_invalid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand());

            commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.BAD_REQUEST);
		}

		[Fact]		
		public void Should_contains_success_status_code_in_command_result_when_user_comand_is_valid()
		{
			var commandResult = this.brandHandler.Create(new BrandCommand() { Name = this.MockString() });
            
		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
		}
	}
}
