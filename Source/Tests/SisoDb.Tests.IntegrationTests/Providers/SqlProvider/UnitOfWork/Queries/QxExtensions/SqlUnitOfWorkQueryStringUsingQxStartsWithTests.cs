﻿using System;
using System.Linq;
using NUnit.Framework;
using SisoDb.Querying;

namespace SisoDb.Tests.IntegrationTests.Providers.SqlProvider.UnitOfWork.Queries.QxExtensions
{
    [TestFixture]
    public class SqlUnitOfWorkQueryStringUsingQxStartsWithTests : IntegrationTestBase
    {
        protected override void OnTestFinalize()
        {
            DropStructureSet<QxItemForQueries>();
        }

        private class QxItemForQueries
        {
            public Guid Id { get; set; }

            public string StringValue { get; set; }
        }

        [Test]
        public void QueryStringUsingQxStartsWith_NoMatch_NullIsReturned()
        {
            var item = new QxItemForQueries { StringValue = "ABC" };
            using (var uow = Database.CreateUnitOfWork())
            {
                uow.Insert(item);
                uow.Commit();
            }

            QxItemForQueries refetched;
            using (var uow = Database.CreateUnitOfWork())
            {
                refetched = uow.Query<QxItemForQueries>(i => i.StringValue.QxStartsWith("C")).SingleOrDefault();
            }

            Assert.IsNull(refetched);
        }

        [Test]
        public void QueryStringUsingQxStartsWith_MatchingStart_ItemIsReturned()
        {
            var item = new QxItemForQueries { StringValue = "ABC" };
            using (var uow = Database.CreateUnitOfWork())
            {
                uow.Insert(item);
                uow.Commit();
            }

            QxItemForQueries refetched;
            using (var uow = Database.CreateUnitOfWork())
            {
                refetched = uow.Query<QxItemForQueries>(i => i.StringValue.QxStartsWith("AB")).SingleOrDefault();
            }

            Assert.AreEqual("ABC", refetched.StringValue);
        }

        [Test]
        public void QueryStringUsingQxStartsWith_CompleteMatch_ItemIsReturned()
        {
            var item = new QxItemForQueries { StringValue = "ABC" };
            using (var uow = Database.CreateUnitOfWork())
            {
                uow.Insert(item);
                uow.Commit();
            }

            QxItemForQueries refetched;
            using (var uow = Database.CreateUnitOfWork())
            {
                refetched = uow.Query<QxItemForQueries>(i => i.StringValue.QxStartsWith("ABC")).SingleOrDefault();
            }

            Assert.AreEqual("ABC", refetched.StringValue);
        }
    }
}