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
		public ICommandResult Delete(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var assetId))
				{
					var removed = this.assetRepository.Delete(assetId);
					if (removed)
					{
						return new SuccessfulCommandResult($"The asset {assetId} was removed with successful", null);
					}

					return new UnsuccessfulCommandResult(StatusCodeResult.NOT_FOUND,
						$"The asset {assetId} not found", null);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"The asset identifier should be a valid GUID", null);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.INTERNAL_SERVER_ERROR,
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}
	}
}
