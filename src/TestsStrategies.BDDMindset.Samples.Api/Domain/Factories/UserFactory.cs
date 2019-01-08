using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Factories
{
	public class UserFactory
	{
		public static User Create(UserCommand command)
		{
			var name = new Name(command.FirstName, command.LastName);
			var email = new Email(command.Email);
			var password = new SecurityPassword(command.Password);
			var user = new User(name, email, password);

			return user;
		}
	}
}
