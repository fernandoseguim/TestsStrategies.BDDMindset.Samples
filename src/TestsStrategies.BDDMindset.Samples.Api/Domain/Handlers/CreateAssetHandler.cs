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
	public partial class AssetHandler : Notifiable, ICommandHandler<AssetCommand>
	{
		private readonly IAssetRepository assetRepository;
		private readonly IBrandRepository brandRepository;

		public AssetHandler(IAssetRepository repository, IBrandRepository brandRepository)
		{
			this.assetRepository = repository;
			this.brandRepository = brandRepository;
		}

		public ICommandResult Create(AssetCommand command)
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

					var brand = BrandFactory.Create(brandResult);
					var asset = AssetFactory.Create(command, brand);

					this.assetRepository.Save(asset);

					return new SuccessfulCommandResult("Asset was saved with successful", new
					{
						id = asset.Id,
						Name = asset.Name,
						Description = asset.Description,
						Registry = asset.Registry,
						Brand = new
						{
							id = asset.Brand.Id,
							Name = asset.Brand.Name,
						}
					});
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"Please, check the properties before creating the asset", command.Notifications);
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
