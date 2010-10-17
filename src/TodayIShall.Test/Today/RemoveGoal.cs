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

namespace TodayIShall.Test.Today
{
    [TestFixture]
    public class RemoveGoal : TestBase
    {
        private Feature remove_goal = new Story("Removing a goal")
            .InOrderTo("complete my list of today's goals")
            .AsA("user")
            .IWant("to remove an existing goal");

        private string description;
        private Account account;
        private IDocumentService _documentService;
        private TodayController controller;
        private ActionResult result;
        private CalendarDay day;

        [Test]
        public void NewGoal()
        {
            remove_goal.WithScenario("remove goal")
                .Given(TheGoal, "eat a goat")
                .And(IAmFromChicago)
                .And(TheDayIs, new CalendarDay(2010, 09, 08))
                .When(IRemoveTheGoal)
                .Then(TheGoalIsRemoved)
                .Execute();
        }

        private void TheGoal(string goal)
        {
            account = Build.A<Account>(a => a.Goals.Add(new Goal(goal, new CalendarDay(2010, 9, 8))));
        }

        private void IAmFromChicago()
        {
            // default account is Central Standard Time (GMT -6)
        }

        private void TheDayIs(CalendarDay day)
        {
            this.day = day;
            _documentService = Substitute.For<IDocumentService>();
            _documentService.Query(Arg.Any<AccountByNameSlug>()).Returns((new List<Account> { account }).AsQueryable());
            controller = new TodayController(_documentService);
        }

        private void IRemoveTheGoal()
        {
            Console.WriteLine(account.Goals.First().Day.ToDateTime().ToShortDateString());
            Console.WriteLine(day.ToDateTime().ToShortDateString());
            result = controller.RemoveGoal("This-Is-Not-Important", account.Goals.First().Id);
        }

        private void TheGoalIsRemoved()
        {
            Assert.AreEqual(0, account.Goals.Count);
            //DocumentService.Received().Session.SaveChanges();
        }
    }
}
