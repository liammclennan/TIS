using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;
using StoryQ;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Domain;
using TodayIShall.Core.Queries.AccountQueries;
using TodayIShall.Web.Controllers;
using TodayIShall.Web.Models;

namespace TodayIShall.Test.Today
{
    [TestFixture]
    public class AddGoal : TestBase
    {
        private Feature add_goal = new Story("Adding a goal")
            .InOrderTo("complete my list of today's goals")
            .AsA("user")
            .IWant("to add a new goal");

        private string description;
        private Account account;
        private IDocumentService _documentService;
        private TodayController controller;
        private ActionResult result;
        private CalendarDay day;

        [Test]
        public void NewGoal()
        {
            add_goal.WithScenario("new goal")
                .Given(TheGoal, "eat a goat")
                .And(IAmFromChicago)
                .And(TheDayIs, new CalendarDay(2010, 09, 08))
                .When(IAddMyNewGoal)
                .Then(TheNewGoalIsAdded)
                .And(TheNewGoalHasTheDate, new CalendarDay(2010, 09, 08)).Execute();
        }

        private void TheGoal(string goal)
        {
            description = goal;
        }

        private void IAmFromChicago()
        {
            account = Build.A<Account>(); // default account is Central Standard Time (GMT -6)
        }

        private void TheDayIs(CalendarDay day)
        {
            this.day= day;
            _documentService = Substitute.For<IDocumentService>();
            _documentService.Query(Arg.Any<AccountByNameSlug>()).Returns((new List<Account> {account}).AsQueryable());
            controller = new TodayController(_documentService);
        }

        private void IAddMyNewGoal()
        {
            result =
                controller.AddGoal(new AddRemoveGoalModel
                                       {
                                           goal = description,
                                           year = day.Year,
                                           month = day.Month,
                                           day = day.Day
                                       });
        }

        private void TheNewGoalIsAdded()
        {
            Assert.AreEqual(1, account.Goals.Count);
            Assert.AreEqual(description, account.Goals[0].Description);
        }

        private void TheNewGoalHasTheDate(CalendarDay goalDate)
        {
            Assert.AreEqual(goalDate, account.Goals[0].Day);
        }
    }
}
