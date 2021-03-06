using NUnit.Framework;
using SisoDb.Providers.SqlProvider;

namespace SisoDb.Tests.IntegrationTests.Providers.SqlProvider
{
    [TestFixture]
    public abstract class SqlIntegrationTestBase : IntegrationTestBase
    {
        protected DbHelper DbHelper { get; private set; }

        protected SqlIntegrationTestBase()
        {
            DbHelper = new DbHelper(Database.ConnectionInfo.ConnectionString.PlainString);
        }

        protected string GetStructureTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema<T>().GetStructureTableName();
        }

        protected string GetIndexesTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema<T>().GetIndexesTableName();
        }

        protected string GetUniquesTableName<T>() where T : class
        {
            return Database.StructureSchemas.GetSchema<T>().GetUniquesTableName();
        }
    }
}