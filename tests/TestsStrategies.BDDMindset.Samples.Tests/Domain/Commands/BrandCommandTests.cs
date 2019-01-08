using FluentAssertions;
using System.Linq;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Commands
{
    public class BrandCommandTests : UnitTestBase
	{
	    [Fact]
	    public void Should_not_contain_notifications_when_creating_a_new_brand_with_valid_requides_properties()
	    {
	        var brandCommand = new BrandCommand { Name = this.MockString(Brand.MINIMUM_NAME_SIZE) };

	        brandCommand.IsValid();

	        brandCommand.Notifications.ToList().Should().BeEmpty();
	    }

        [Fact]
		public void Should_contain_a_notification_when_creating_a_new_brand_and_brand_name_is_null()
		{
			var brandCommand = new BrandCommand();

		    brandCommand.IsValid();

		    brandCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(brandCommand.Name)));    
		}

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_brand_with_a_brand_name_lower_than_the_minimum_allowed_size()
		{
			var brandCommand = new BrandCommand { Name = this.MockString(Brand.MINIMUM_NAME_SIZE - 1) };

		    brandCommand.IsValid();

		    brandCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(brandCommand.Name)));
        }

		[Fact]
		public void Should_contain_a_notification_when_creating_a_new_brand_with_a_brand_name_greater_than_the_maximum_allowed_size()
		{
			var brandCommand = new BrandCommand { Name = this.MockString(Brand.MAXIMUM_NAME_SIZE + 1) };

		    brandCommand.IsValid();

		    brandCommand.Notifications.ToList().Should().Contain(n => n.Property.Contains(nameof(brandCommand.Name)));
        }
	}
}
