using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodayIShall.Core.Domain;

namespace TodayIShall.Test
{
    public class TestBase
    {
        public Builder Build
        {
            get 
            { 
                var builder = new Builder();
                builder.Configure(new Dictionary<Type, Func<object>>
                {
                    {typeof(Account), () =>
                                          {
                                              var account = new Account { Email = "bob@yaoo.com", FirstName = "Bob", LastName = "Geldof", TimeZoneInfoId = "Central Standard Time", Password = "password"};
                                              account.SetNameSlug();
                                              return account;
                                          }}                          



                });
                return builder;
            }
        }

    }
}
