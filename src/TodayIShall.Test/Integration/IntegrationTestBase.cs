using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using NUnit.Framework;
using TodayIShall.Core.AppServices;

namespace TodayIShall.Test.Integration
{
    public class IntegrationTestBase : TestBase
    {
        protected IDocumentService DocumentService;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var configuration = new ConfigService();
            DocumentService = new DocumentService(Mongo.Create(configuration.MongoConnectionString));
            CleanDB();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            DocumentService.Mongo.Dispose();
        }

        protected void CleanDB()
        {
            if (!DataBaseHasBeenReset())
            {
                ResetDatabase();
            }
        }

        private void ResetDatabase()
        {
            databaseHasBeenReset = true;
            var testData = new TestData(DocumentService);
            testData.Rebuild();
        }

        private static bool databaseHasBeenReset;
        protected bool DataBaseHasBeenReset()
        {
            return databaseHasBeenReset;
        }
    }
}
