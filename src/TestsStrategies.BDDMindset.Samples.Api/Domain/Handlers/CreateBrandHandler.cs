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
	public partial class BrandHandler : Notifiable, ICommandHandler<BrandCommand>
	{
		private readonly IBrandRepository repository;

		public BrandHandler(IBrandRepository repository) => this.repository = repository;

		public ICommandResult Create(BrandCommand command)
		{
			try
			{
				if (command.IsValid())
				{
					var brand = BrandFactory.Create(command);
					
					if (this.CheckBrand(brand.Name))
					{
						return new UnsuccessfulCommandResult(StatusCodeResult.CONFLICT,
							"Please, check the properties before creating the user", this.Notifications);
					}

					this.repository.Save(brand);

					return new SuccessfulCommandResult("Brand was saved with successful", new
					{
						id = brand.Id,
						Name = brand.Name,
					});
				}

				return new UnsuccessfulCommandResult(StatusCodeResult.BAD_REQUEST,
					"Please, check the properties before creating the brand", command.Notifications);
			}
			catch (Exception ex)
			{
				return new UnsuccessfulCommandResult(StatusCodeResult.INTERNAL_SERVER_ERROR,
					"There was an error saving the user, please contact your system administrator.",
					ex.Message);
			}
		}

		public bool CheckBrand(string name)
		{
			var brandAlreadyExists = this.repository.CheckBrand(name);

			if (brandAlreadyExists)
			{
				this.AddNotification("Name", "Brand already exists on database");
			}
			return brandAlreadyExists;
		}
	}
}
