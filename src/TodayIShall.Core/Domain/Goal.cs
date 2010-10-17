using System;

namespace TodayIShall.Core.Domain
{
    public class Goal
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool Done { get; private set; }
        public CalendarDay Day { get; set; }
        
        public Goal()
        {
            Id = Guid.NewGuid();
        }

        public Goal(string description, CalendarDay day) : this()
        {
            Description = description;
            Day = day;
            Done = false;
        }

        public void Do()
        {
            Done = true;
        }
        
        public void Undo()
        {
            Done = false;
        }
    }
}