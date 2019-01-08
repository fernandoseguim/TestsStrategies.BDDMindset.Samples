using FluentAssertions;
using System;
using System.Linq;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Entities
{
    public class AssetTests : UnitTestBase
	{
		private readonly Brand brand;
		public AssetTests() => 
			this.brand = new Brand(base.MockString());

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_asset_with_null_name()
		{
		    WhenInstance<Asset>(() => new Asset(null, this.brand))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_argument_null_exception_when_trying_to_create_a_new_asset_with_empty_name()
		{
		    WhenInstance<Asset>(() => new Asset(string.Empty, this.brand))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_generate_a_register_number_when_trying_to_create_a_new_asset_with_null_brand()
		{
		    WhenInstance<Asset>(() => new Asset(base.MockString(), null))
		       .Should()
		       .Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_generate_a_register_number_when_trying_to_create_a_new_asset_with_valid_name()
		{
			var asset = new Asset(base.MockString(), this.brand);

		    asset.Registry.Should().NotBeEmpty();
		}

	    [Fact]
	    public void Given_that_register_number_is_generated_when_create_asset_then_the_register_number_length_should_be_equal_to_max_length()
	    {
            var asset = new Asset(base.MockString(), this.brand);

	        asset.Registry.Length.Should().Be(Asset.REGISTER_NAME_MAX_LENGTH);
	    }

        [Fact]
		public void Should_add_description_when_description_length_is_lower_or_equal_to_the_maximum_allowed_size()
		{
			var asset = new Asset(base.MockString(), this.brand);
			var description = this.MockString(10);

			asset.AddDescription(description);

		    asset.Description.Should().BeEquivalentTo(description);
		}

		[Fact]
		public void Should_contain_a_notification_when_add_description_with_length_greater_than_the_maximum_allowed_size()
		{
			var asset = new Asset(base.MockString(), this.brand);
			var description = this.MockString(Asset.MAXIMUM_DESCRIPTION_SIZE + 1);

			asset.AddDescription(description);

		    asset.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(asset.Description)));
		}
	}
}
