using Flunt.Validations;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Extensions.Flunt;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request
{
	public class AssetCommand : BaseCommand
	{
		public string Name { get; set; }
		public string BrandId { get; set; }
		public string Description { get; set; }

		public override bool IsValid()
		{
			base.AddNotifications(new Contract()
				.Requires()
				.HasMinLength(this.Name, Asset.MINIMUM_NAME_SIZE, nameof(this.Name),
					$"The asset name should contain a minimum of {Asset.MINIMUM_NAME_SIZE} characters")
				.HasMaxLength(this.Name, Asset.MAXIMUM_NAME_SIZE, nameof(this.Name),
					$"The asset name should contain a maximum of {Asset.MAXIMUM_NAME_SIZE} characters")
				.IsValidGuid(this.BrandId,nameof(this.BrandId), 
					"The brand identifier should be a valid GUID")
			);

			return this.Valid;
		}
	}
}
