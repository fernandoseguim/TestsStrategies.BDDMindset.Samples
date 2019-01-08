using FluentAssertions;
using System.Linq;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Commands
{
    public class UserCommandTests : UnitTestBase
    {
        [Fact]
        public void Should_not_contain_notifications_when_creating_a_new_user_with_valid_requides_properties()
        {
            var userCommand = new UserCommand
            {
                FirstName = this.MockString(Name.MINIMUM_FIRST_NAME_SIZE),
                LastName = this.MockString(Name.MINIMUM_LAST_NAME_SIZE),
                Email = "fernando.seguim@gmail.com",
                Password = this.MockString(SecurityPassword.MINIMUM_PASSWORD_SIZE)
            };

            userCommand.IsValid();

            userCommand.Notifications.ToList().Should().BeEmpty();
        }

        [Fact]
        public void Should_contain_a_notification_when_creating_a_new_user_and_first_name_is_null()
        {
            var userCommand = new UserCommand();

            userCommand.IsValid();

            userCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(userCommand.FirstName)));
        }

        [Fact]
        public void Should_contain_a_notification_when_creating_a_new_user_and_last_name_is_null()
        {
            var userCommand = new UserCommand();

            userCommand.IsValid();

            userCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(userCommand.LastName)));
        }

        [Fact]
        public void Should_contain_a_notification_when_creating_a_new_user_and_first_name_is_lower_than_min_allowed_size()
        {
            var userCommand = new UserCommand { FirstName = this.MockString(Name.MINIMUM_FIRST_NAME_SIZE - 1) };

            userCommand.IsValid();

            userCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(userCommand.FirstName)));
        }

        [Fact]
        public void Should_contain_a_notification_when_creating_a_new_user_and_last_name_is_lower_than_min_allowed_size()
        {
            var userCommand = new UserCommand { LastName = this.MockString(Name.MINIMUM_LAST_NAME_SIZE - 1) };

            userCommand.IsValid();

            userCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(userCommand.LastName)));
        }
    }
}
