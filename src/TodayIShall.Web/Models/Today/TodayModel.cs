using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Domain;

namespace TodayIShall.Web.Models
{
    public class TodayModel
    {
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameSlug { get; set; }
        public DateTime AccountDay { get;set; }
        public IEnumerable<Goal> Goals { get; private set; }

        public void BindTo(Account account)
        {
            Mapper.Map(account, this);
            AccountDay = DateTime.Now.ToUniversalTime().Add(account.TimeZone().BaseUtcOffset).Date;
            Goals = account.GoalsFor(new CalendarDay(AccountDay));
        }
        
        public void BindTo(Account account, CalendarDay day)
        {
            Mapper.Map(account, this);
            Goals = account.GoalsFor(day);
            AccountDay = day.ToDateTime();
        }

        public object DayAsRouteValues()
        {
            return new {year = AccountDay.Year, month = AccountDay.Month, day = AccountDay.Day};
        }
    }
}