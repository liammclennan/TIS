using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Domain;

namespace TodayIShall.Test.Integration
{
    public class TestData
    {
        private readonly IDocumentService _documentService;

        public TestData(IDocumentService _documentService)
        {
            this._documentService = _documentService;
        }

        public void Rebuild()
        {
            Drop();
            Create();
        }

        private void Drop()
        {
            foreach (var ci in _documentService.Mongo.Database.GetAllCollections())
            {
                Console.WriteLine(ci.Name);
                if (ci.Name.Equals("tis.Account"))
                    _documentService.Mongo.Database.DropCollection(ci.Name.Split('.')[1]);
            }
        }

        private void Create()
        {
            CreateAccounts();
        }

        private void CreateAccounts()
        {
            var test = new TestBase();
            _documentService.Save(test.Build.A<Account>(a => a.NameSlug = "Amory-Blaine"));
            var liam = new Account
                           {
                               FirstName = "Liam",
                               LastName = "McLennan",
                               Email = "liam.mclennan@gmail.com",
                               TimeZoneInfoId = "E. Australia Standard Time",
                               Password = "password"
                           };
            liam.SetNameSlug();
            _documentService.Save(liam);
        }
    }
}
