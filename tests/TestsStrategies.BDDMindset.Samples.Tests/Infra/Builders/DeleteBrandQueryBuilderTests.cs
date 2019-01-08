//using System;
//using System.Linq;
//using POC.CQRS.Infra.Builders;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TestsStrategies.BDDMindset.Samples.Tests;

//namespace POC.CQRS.Tests.Infra.Builders
//{
//	public partial class BrandQueryBuilderTests : UnitTestBase
//    {
//		[Fact]
//		[Description("Given that I want delete a brand, " +
//		             "when building a delete brand query, " +
//		             "then should contains brand identifier into statement and parameters")]
//		public void Should_contains_brand_identifier_into_statement_and_parameters_when_building_a_delete_brand_query()
//		{
//			var brandId = Guid.NewGuid();
//			var (query, parameters) = new BrandQueryBuilder()
//				.DeleteBrand(brandId)
//				.Build();

//			Assert.IsTrue(query.Contains("BrandID") && query.Contains(@"@BrandID"));
//			Assert.IsTrue(parameters.ParameterNames.Contains("BrandID"));
//			Assert.AreEqual(brandId, parameters.Get<Guid>("BrandID"));
//		}
//	}
//}
