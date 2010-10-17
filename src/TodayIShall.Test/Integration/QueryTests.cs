using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TodayIShall.Core.Queries.AccountQueries;

namespace TodayIShall.Test.Integration
{
    [TestFixture]
    public class QueryTests : IntegrationTestBase
    {
        [Test]
        public void Do()
        {
            var account = DocumentService.Query(new AccountByNameSlug("Amory-Blaine")).First();
        }
    }
}
