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
		public ICommandResult Delete(string id)
		{
			try
			{
				if (Guid.TryParse(id, out var brandId))
				{
					var removed = this.repository.Delete(brandId);
					if (removed)
					{
						return new SuccessfulCommandResult($"The brand {brandId} was removed with successful", null);
					}

					return new UnsuccessfulCommandResult(StatusCodeResult.NOT_FOUND,
						$"The brand {brandId} not found", null);
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"The brand identifier should be a valid GUID", null);
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
