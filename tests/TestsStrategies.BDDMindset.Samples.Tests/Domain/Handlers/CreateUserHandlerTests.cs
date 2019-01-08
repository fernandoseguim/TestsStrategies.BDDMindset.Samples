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
	public partial class UserHandlerTests : UnitTestBase
	{
		private readonly string notFoundUserId;
		private readonly IUserRepository repository;
		private readonly ICommandHandler<UserCommand> userHandler;
		private readonly UserCommand userCommand;

		public UserHandlerTests()
		{
            this.notFoundUserId = this.Fixture.Create<Guid>().ToString();

            this.userCommand = new UserCommand();
			this.repository = Substitute.For<IUserRepository>();
			this.userHandler = new UserHandler(this.repository);

		    this.userCommand.FirstName = this.MockString();
			this.userCommand.LastName = this.MockString();
			this.userCommand.Email = $"{this.MockString()}@teste.com";
			this.userCommand.Password = this.MockString();
			
			this.repository.CheckEmail(Arg.Is<Email>(email => email.ToString().Equals("teste@gmail.com"))).Returns(true);
			this.repository.CheckEmail(Arg.Is<Email>(email => !email.ToString().Equals("teste@gmail.com"))).Returns(false);
			this.repository.Delete(Arg.Any<Guid>()).Returns(true);
			this.repository.Delete(Arg.Is<Guid>(id => id.Equals(new Guid(this.notFoundUserId)))).Returns(false);
		}

		[Fact]
		public void Should_contains_conflict_status_code_in_command_result_when_email_already_exists_in_database()
		{
			this.userCommand.Email = $"teste@gmail.com";
			var commandResult = this.userHandler.Create(this.userCommand);

            commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.CONFLICT);
		}

		[Fact]
		public void Should_contains_notification_in_command_result_when_email_already_exists_in_database()
		{
			this.userCommand.Email = $"teste@gmail.com";
			var commandResult = this.userHandler.Create(this.userCommand);

			var notifications = (List<Notification>) commandResult.Data;

		    notifications.First().Property.Should().Be("email");
		}

		[Fact]
		public void Should_contains_notifications_command_result_when_user_comand_is_invalid()
		{
			var commandResult = this.userHandler.Create(new UserCommand());

			var notifications = (List<Notification>)commandResult.Data;

		    notifications.Should().NotBeEmpty();
        }

		[Fact]
		public void Should_contains_bad_request_status_code_in_command_result_when_user_comand_is_invalid()
		{
			var commandResult = this.userHandler.Create(new UserCommand());

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.BAD_REQUEST);
		}

		[Fact]
		public void Should_contains_success_status_code_in_command_result_when_user_comand_is_valid()
		{
			var commandResult = this.userHandler.Create(this.userCommand);

		    commandResult.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
        }
	}
}
