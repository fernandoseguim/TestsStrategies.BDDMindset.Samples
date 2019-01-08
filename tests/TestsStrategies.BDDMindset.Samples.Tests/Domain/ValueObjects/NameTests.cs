//using System;
//using POC.CQRS.Domain.ValueObjects;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TestsStrategies.BDDMindset.Samples.Tests;

//namespace POC.CQRS.Tests.Domain.ValueObjects
//{
//	[TestClass]
//	public class NameTests : UnitTestBase
//	{
//		[Fact]
//		[Description("Given that firt name is null, " +
//		             "when trying to create a new name, " +
//		             "then should throw argument exception")]
//		public void Should_throw_argument_exception_when_creating_a_new_name_and_first_name_is_null()
//		{
//			Assert.ThrowsException<ArgumentException>(() => new Name(null, this.MockString()));
//		}

//		[Fact]
//		[Description("Given that last name is null, " +
//		             "when trying to create a new name, " +
//		             "then should throw argument exception")]
//		public void Should_throw_argument_exception_when_creating_a_new_name_and_last_name_is_null()
//		{
//			Assert.ThrowsException<ArgumentException>(() => new Name(this.MockString(), null));
//		}

//		[Fact]
//		[Description("Given that name is created, " +
//		             "when trying to convert name to string, " +
//		             "then should return fist name and last name")]
//		public void Should_return_fist_name_and_last_name_when_convert_name_to_string()
//		{
//			var fisrtname = this.MockString();
//			var lastname = this.MockString();
//			var name = new Name(fisrtname, lastname);

//			Assert.IsTrue(name.ToString().Contains(fisrtname));
//			Assert.IsTrue(name.ToString().Contains(lastname));
//		}
//	}
//}
