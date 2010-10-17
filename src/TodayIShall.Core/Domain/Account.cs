using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TodayIShall.Core.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZoneInfoId { get; set; }
        public string NameSlug { get; set; }
        public string Password { get; set; }
        public IList<Goal> Goals { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
            Goals = new List<Goal>();
        }

        public void SetNameSlug()
        {
            var joined = FirstName + "-" + LastName;
            joined = joined.Replace(" ", "-");
            NameSlug = HttpUtility.UrlEncode(joined);
            Console.WriteLine(NameSlug); 
        }

        public void AddGoal(string description, CalendarDay day)
        {
            if (description == null) return;
            var goal = new Goal(description.Trim(), day);
            Goals.Add(goal);
        }

        public TimeZoneInfo TimeZone()
        {
            return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfoId);
        }

        public void RemoveGoal(Guid Id)
        {
            Goals = Goals.Where(g => g.Id != Id).ToList();
        }

        public void Done(Guid Id)
        {
            Goal goal = GetGoal(Id);
            if (goal == null) return;
            goal.Do();
        }

        public void Undone(Guid Id)
        {
            Goal goal = GetGoal(Id);
            if (goal == null) return;
            goal.Undo();
        }

        private Goal GetGoal(Guid Id)
        {
            return Goals.FirstOrDefault(g => g.Id == Id);
        }

        public bool IsCorrectPassword(string password)
        {
            return Password.Equals(password);
        }

        public IEnumerable<Goal> GoalsFor(CalendarDay calendarDay)
        {
            return Goals.Where(goal => goal.Day.Equals(calendarDay));
        }

        public void CopyForward(CalendarDay day)
        {
            var incomplete = GoalsFor(day).Where(goal => !goal.Done).ToList();
            var nextDay = day.AddDays(1);
            foreach (var goal in incomplete)
            {
                AddGoal(goal.Description, nextDay);   
            }
        }
    }
}
