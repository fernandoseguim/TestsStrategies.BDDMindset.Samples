using System;
using System.Linq;
using FluentAssertions;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Commands
{
	public class AssetCommandTests : UnitTestBase
	{
		[Fact]
		public void Should_not_contain_notifications_when_creating_a_new_asset_with_valid_requides_properties()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE),
				BrandId = Guid.NewGuid().ToString()
			};

		    assetCommand.IsValid();
		    
		    assetCommand.Notifications.ToList().Should().BeEmpty();
		}

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_asset_and_asset_name_is_null()
		{
			var assetCommand = new AssetCommand
			{
				BrandId = Guid.NewGuid().ToString()
			};

		    assetCommand.IsValid();
            
		    assetCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(assetCommand.Name)));
		}

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_asset_with_a_asset_name_lower_than_the_minimum_allowed_size()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE - 1),
				BrandId = Guid.NewGuid().ToString()
			};

		    assetCommand.IsValid();

            assetCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(assetCommand.Name)));
        }

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_asset_with_a_asset_name_greater_than_the_maximum_allowed_size()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MAXIMUM_NAME_SIZE + 1),
				BrandId = Guid.NewGuid().ToString()
			};

		    assetCommand.IsValid();
            
		    assetCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(assetCommand.Name)));
        }

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_asset_with_a_invalid_brand_identifier()
		{
			var assetCommand = new AssetCommand
			{
				Name = this.MockString(Asset.MINIMUM_NAME_SIZE),
				BrandId = Guid.NewGuid().ToString().Substring(0, 9)
			};

		    assetCommand.IsValid();

		    assetCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(assetCommand.BrandId)));
		}		
	}
}
