using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;

namespace TestsStrategies.BDDMindset.Samples.Api.Domain.Factories
{
	public class AssetFactory
	{
		public static Asset Create(AssetCommand command, Brand brand)
		{
			var asset = new Asset(command.Name, brand);
			asset.AddDescription(command.Description);
			return asset;
		}
	}
}