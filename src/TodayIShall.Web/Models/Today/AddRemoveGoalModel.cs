using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIShall.Core.Domain;

namespace TodayIShall.Web.Models
{
    public class AddRemoveGoalModel
    {
        public string goal { get; set;}
        public string NameSlug { get; set;}
        public int year { get; set;}
        public int month { get; set;}
        public int day { get; set;}

        public CalendarDay CalendarDay 
        {
            get { return new CalendarDay(year, month,day);}
        } 
    }
}