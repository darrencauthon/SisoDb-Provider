﻿using NUnit.Framework;
using SisoDb.Lambdas.Processors;
using SisoDb.Querying;

namespace SisoDb.Tests.UnitTests.Lambdas.ParsedLambdaSqlProcessorTests
{
    [TestFixture]
    public class ParsedLambdaSqlProcessorHierarchyTests : ParsedLambdaSqlProcessorTestBase
    {
        [Test]
        public void Process_WhenFirstLevel_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenFirstLevel_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] {new QueryParameter("@p0", 42)};
            AssertQueryParameters(expectedParameters, query.Parameters);
        }

        [Test]
        public void Process_WhenNested_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[NestedItem.Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenNested_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] { new QueryParameter("@p0", 42) };
            AssertQueryParameters(expectedParameters, query.Parameters);
        }

        [Test]
        public void Process_WhenDeepNested_GeneratesCorrectSqlQuery()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.SuperNestedItem.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            const string expectedSql = "si.[NestedItem.SuperNestedItem.Int1] = @p0";
            Assert.AreEqual(expectedSql, query.Sql);
        }

        [Test]
        public void Process_WhenDeepNested_ExtractsCorrectParameters()
        {
            var parsedLambda = CreateParsedLambda<MyItem>(i => i.NestedItem.SuperNestedItem.Int1 == 42);

            var processor = new ParsedLambdaSqlProcessor(new MemberNameGeneratorFake());
            var query = processor.Process(parsedLambda);

            var expectedParameters = new[] { new QueryParameter("@p0", 42) };
            AssertQueryParameters(expectedParameters, query.Parameters);
        }
    }
}