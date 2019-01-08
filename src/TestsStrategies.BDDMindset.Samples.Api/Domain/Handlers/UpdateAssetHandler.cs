using System;
using Flunt.Notifications;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Contracts;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers
{
	public partial class AssetHandler : Notifiable, ICommandHandler<AssetCommand>
	{
		public ICommandResult Update(string id, AssetCommand command)
		{
			try
			{
				if (command.IsValid())
				{
					var brandResult = this.brandRepository.GetById(new Guid(command.BrandId));

					if (brandResult is null)
					{
						this.AddNotification("BrandID", "Brand not found");
						return new UnsuccessfulCommandResult(StatusCodeResult.NOT_FOUND,
							"Please, check the properties before creating the user", this.Notifications);
					}

					var updated = this.assetRepository.Update(new Guid(id), command);

					if (updated)
					{
						return new SuccessfulCommandResult("Asset was updated with successful",
							new { UserId = id, Name = command.Name, Description = command.Description });
					}

					this.AddNotification("Id", $"The brand {id} not found");
					return new UnsuccessfulCommandResult(StatusCodeResult.NOT_FOUND,
						"Please, check the properties before updating the asset", this.Notifications);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"Please, check the properties before updating the asset", command.Notifications);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.INTERNAL_SERVER_ERROR,
					"There was an error saving the user, please contact your system administrator.", ex.Message);
			}
		}
	}
}
