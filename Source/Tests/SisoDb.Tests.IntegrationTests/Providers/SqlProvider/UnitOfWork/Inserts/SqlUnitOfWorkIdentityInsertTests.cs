using System.Collections.Generic;
using System.Data;
using NUnit.Framework;

namespace SisoDb.Tests.IntegrationTests.Providers.SqlProvider.UnitOfWork.Inserts
{
    [TestFixture]
    public class SqlUnitOfWorkIdentityInsertTests : SqlIntegrationTestBase
    {
        protected override void OnTestFinalize()
        {
            DropStructureSet<ItemForIdentityInserts>();
            DropStructureSet<ItemForIdentityInsertsWithPrivateSetter>();
        }

        [Test]
        public void Insert10Items_WhenNoItemsExists_CurrentIdInIdentitiesTableIs10()
        {
            var items = new List<ItemForIdentityInserts>();
            for (var c = 0; c < 10; c++)
                items.Add(new ItemForIdentityInserts());

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.InsertMany(items);
                uow.Commit();
            }

            var currentId = DbHelper.ExecuteScalar<int>(CommandType.Text, "select CurrentId from dbo.SisoDbIdentities where EntityName = 'ItemForIdentityInserts';");
            Assert.AreEqual(10, currentId);
        }

        [Test]
        public void Insert_WhenIdSetterIsPrivate_ItemIsInsertedAndIdIsAssigned()
        {
            var item = new ItemForIdentityInsertsWithPrivateSetter();

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.Insert(item);
                uow.Commit();
            }

            var assignedId = 1;
            using (var uow = Database.CreateUnitOfWork())
            {
                item = uow.GetById<ItemForIdentityInsertsWithPrivateSetter>(assignedId);
            }

            Assert.AreEqual(assignedId, item.Id);
        }

        [Test]
        public void Insert_WhenNoNullableIdIsAssignedToItem_IdIsAutomaticallyAssigned()
        {
            var item = new ItemForNullableIdentityInsertsWithPrivateSetter();

            using (var uow = Database.CreateUnitOfWork())
            {
                uow.Insert(item);
                uow.Commit();
            }

            using (var uow = Database.CreateUnitOfWork())
            {
                item = uow.GetById<ItemForNullableIdentityInsertsWithPrivateSetter>(item.Id.Value);
            }

            Assert.IsNotNull(item.Id);
            Assert.AreEqual(1, item.Id);
        }

        private class ItemForIdentityInserts
        {
            public int Id { get; set; }

            public string Temp
            {
                get { return "Some text to get rid of exception of no indexable members."; }
            }
        }

        private class ItemForIdentityInsertsWithPrivateSetter
        {
            public int Id { get; private set; }

            public string Temp
            {
                get { return "Some text to get rid of exception of no indexable members."; }
            }
        }

        private class ItemForNullableIdentityInsertsWithPrivateSetter
        {
            public int? Id { get; private set; }

            public string Temp
            {
                get { return "Some text to get rid of exception of no indexable members."; }
            }
        }
    }
}