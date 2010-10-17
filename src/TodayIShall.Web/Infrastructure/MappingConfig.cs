using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TodayIShall.Core.Domain;
using TodayIShall.Web.Models;

namespace TodayIShall.Web.Infrastructure
{
    /// <summary>
    /// Configure AutoMapping
    /// </summary>
    public class MappingConfig
    {
        public void Initialize()
        {
            Mapper.CreateMap<NewAccountModel, Account>().AfterMap((m, a) => a.SetNameSlug());
            Mapper.CreateMap<Account, TodayModel>()
                .ForMember(m => m.AccountId, opt => opt.MapFrom(a => a.Id));
        }
    }

}