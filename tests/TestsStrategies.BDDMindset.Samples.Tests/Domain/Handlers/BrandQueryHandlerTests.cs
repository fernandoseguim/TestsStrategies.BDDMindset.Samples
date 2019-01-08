using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Handlers;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Domain.ValueObjects;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories;
using Xunit;

namespace TestsStrategies.BDDMindset.Samples.Tests.Domain.Handlers
{
	public class BrandQueryHandlerTests : UnitTestBase
	{
		private const string NOT_FOUND_USER_ID = "07F28933-A0BE-4C18-847A-346A92497362";
		private readonly IBrandRepository repository;
		private readonly IBrandQueryHandler brandQueryHandler;

		public BrandQueryHandlerTests()
		{
			this.repository = Substitute.For<IBrandRepository>();
			this.brandQueryHandler = new BrandQueryHandler(this.repository);

		    var n = Guid.NewGuid();
		    this.repository.GetAll().Returns(new List<BrandQueryResult>());
		    this.repository.GetById(Arg.Any<Guid>()).Returns(new BrandQueryResult());
		    this.repository.GetById(Arg.Is<Guid>(a => a.Equals(new Guid(NOT_FOUND_USER_ID)))).Returns(default(BrandQueryResult));
        }
        
		[Fact]
		public void Should_return_a_successful_query_result_when_request_all_brands()
		{
			var result = this.brandQueryHandler.GetAll();

		    result.Should().BeOfType<SuccessfulQueryResult>();
		}

		[Fact]
		public void Should_return_a_success_status_code_result_when_request_brand_by_id_and_brand_exists()
		{
			var result = this.brandQueryHandler.GetById(Guid.NewGuid().ToString());

            result.StatusCode.Should().BeEquivalentTo(StatusCodeResult.SUCCESS);
		}

		[Fact]
		public void Should_return_a_not_found_when_request_brand_by_id_and_brand_exists()
		{
			var result = this.brandQueryHandler.GetById(NOT_FOUND_USER_ID);

		    result.StatusCode.Should().BeEquivalentTo(StatusCodeResult.NOT_FOUND);
		}
	}
}
