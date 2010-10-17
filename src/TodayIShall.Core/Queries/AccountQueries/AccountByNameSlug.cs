using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodayIShall.Core.Domain;

namespace TodayIShall.Core.Queries.AccountQueries
{
    public class AccountByNameSlug : IMongoQuery<Account>
    {
        private readonly string nameSlug;

        public AccountByNameSlug(string nameSlug)
        {
            this.nameSlug = nameSlug;
        } 

        public IQueryable<Account> Execute(IQueryable<Account> queryable)
        {
            return queryable.Where(account => account.NameSlug.ToUpper() == nameSlug.ToUpper());
        }
    }
    
    public class AccountById : IMongoQuery<Account>
    {
        private readonly Guid _id;

        public AccountById(Guid id)
        {
            _id = id;
        }

        public IQueryable<Account> Execute(IQueryable<Account> queryable)
        {
            return queryable.Where(account => account.Id == _id);
        }
    }
}
