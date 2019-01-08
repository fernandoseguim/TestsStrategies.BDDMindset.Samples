using System;
using Flunt.Notifications;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
	public partial class BrandHandler : Notifiable, ICommandHandler<BrandCommand>
	{
		public ICommandResult Update(string id, BrandCommand command)
		{
			try
			{
				if (command.IsValid())
				{
					if (this.CheckBrand(command.Name))
					{
						return new UnsuccessfulCommandResult(StatusCodeResult.CONFLICT,
							"Please, check the properties before updating the user", this.Notifications);
					}

					var updated = this.repository.Update(new Guid(id), command);

					if (updated)
					{
						return new SuccessfulCommandResult("Brand was updated with successful", 
							new { UserId = id, Name = command.Name });
					}

					this.AddNotification("Id", $"The brand {id} not found");
					return new UnsuccessfulCommandResult(StatusCodeResult.NOT_FOUND,
						"Please, check the properties before updating the brand", this.Notifications);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"Please, check the properties before updating the brand", command.Notifications);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.INTERNAL_SERVER_ERROR,
					"There was an error saving the user, please contact your system administrator.", ex.Message);
			}
		}
	}
}
