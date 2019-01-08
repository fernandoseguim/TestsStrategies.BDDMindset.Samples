using System;
using Flunt.Notifications;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Factories;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
	public partial class UserHandler : Notifiable, ICommandHandler<UserCommand>
	{
		private readonly IUserRepository repository;

		public UserHandler(IUserRepository repository) => this.repository = repository;

		public ICommandResult Create(UserCommand command)
		{
			try
			{
				if (command.IsValid())
				{	
					var user = UserFactory.Create(command);

					if(this.CheckEmail(user.Email))
					{
						return new UnsuccessfulCommandResult(StatusCodeResult.CONFLICT,
							"Please, check the properties before creating the user", this.Notifications);
					}

					this.repository.Save(user);

					return new SuccessfulCommandResult("User was saved with successful", new
					{
						Id = user.Id,
						Name = user.Name.ToString(),
						Email = user.Email.ToString()
					});
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST, 
					"Please, check the properties before creating the user", command.Notifications);
			}
			catch(Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.INTERNAL_SERVER_ERROR, 
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}

		private bool CheckEmail(Email email)
		{
			var emailAlreadyExists = this.repository.CheckEmail(email);

			if (emailAlreadyExists)
			{
				this.AddNotification(nameof(email), "Email already exists on database");
			}			
			return emailAlreadyExists;
		}
	}
}
