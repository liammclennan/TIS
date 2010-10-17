using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public class CopyForwardScenarios : TestBase
    {
        private Feature copyForward = new Story("Copy forward")
            .InOrderTo("not lose track of a goal")
            .AsA("user")
            .IWant("to be able to copy a day's incomplete goals forward to the next day");

        private Account account;
        private CalendarDay today;
        private CalendarDay tomorrow;
        private TodayController controller;
        private ActionResult result;

        [Test]
        public void Success()
        {
            copyForward.WithScenario("Success")
                .Given(ADayWithSomeCompleteAndSomeIncompleteGoals)
                .When(ICopyForward)
                .Then(TheDaysGoalsShouldBeUnchanged)
                .And(TheFollowingDayShouldHaveTheIncompleteGoals)
                .And(IShouldBeRedirectedToToday)
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        private void ADayWithSomeCompleteAndSomeIncompleteGoals()
        {
            account = Build.An<Account>();
            today = new CalendarDay(2010, 9, 17);
            tomorrow = new CalendarDay(2010, 9, 18);
            account.AddGoal("thing I did", today);
            account.Done(account.Goals.First(g => g.Description.Equals("thing I did")).Id);
            account.AddGoal("thing I should have done", today);
            account.AddGoal("something else I did", today);
            account.Done(account.Goals.First(g => g.Description.Equals("something else I did")).Id);
            account.AddGoal("something I forgot to do", today);
            account.AddGoal("something I will do tomorrow", tomorrow);

            var docService = Substitute.For<IDocumentService>();
            docService.Query(Arg.Any<AccountByNameSlug>()).Returns(new [] {account}.AsQueryable());
            controller = new TodayController(docService);
        }

        private void ICopyForward()
        {
            var model = new ForwardBackModel {year = today.Year, month = today.Month, day = today.Day, nameslug = account.NameSlug};
            result = controller.CopyForward(model);
        }

        private void TheDaysGoalsShouldBeUnchanged()
        {
            var todayGoals = account.GoalsFor(today);
            Assert.AreEqual(4, todayGoals.Count());
            Assert.IsTrue(todayGoals.Any(g => g.Description.Equals("thing I did")));
            Assert.IsTrue(todayGoals.Any(g => g.Description.Equals("thing I should have done")));
            Assert.IsTrue(todayGoals.Any(g => g.Description.Equals("something else I did")));
            Assert.IsTrue(todayGoals.Any(g => g.Description.Equals("something I forgot to do")));
        }

        private void TheFollowingDayShouldHaveTheIncompleteGoals()
        {
            var tomorrowGoals = account.GoalsFor(tomorrow);
            Assert.AreEqual(3, tomorrowGoals.Count());
            Assert.IsTrue(tomorrowGoals.Any(g => g.Description.Equals("thing I should have done")));
            Assert.IsTrue(tomorrowGoals.Any(g => g.Description.Equals("something I forgot to do")));
            Assert.IsTrue(tomorrowGoals.Any(g => g.Description.Equals("something I will do tomorrow")));
        }

        private void IShouldBeRedirectedToToday()
        {
            Assert.AreEqual("Index", ((RedirectToRouteResult) result).RouteValues["action"]);
        }
    }

    
}
