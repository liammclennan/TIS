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
using TodayIShall.Web.Controllers;
using TodayIShall.Web.Infrastructure;
using TodayIShall.Web.Models;

namespace TodayIShall.Test.Registration
{
    [TestFixture]
    public class NewUser
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            new MappingConfig().Initialize();
        }

        protected Feature new_user = new Story("A new user registers")
            .InOrderTo("gain a todayishall account")
            .AsA("anonymous internet user")
            .IWant("to be able to register");

        protected void TheName(string fullName)
        {
            model = new NewAccountModel { FirstName = fullName.Split(' ')[0], LastName = fullName.Split(' ')[1] };
            session = Substitute.For<IDocumentService>();
            cookieGenerator = Substitute.For<IAuthCookieGenerator>();
            controller = new RegistrationController(session, cookieGenerator);
        }

        protected void TheEmailAddress(string email)
        {
            model.Email = email;
        }

        protected void ThePassword(string pw)
        {
            model.Password = pw;
        }

        protected void TheTimeZone(string timeZone)
        {
            model.TimeZoneInfoId = timeZone;
        }

        protected void TheUserRegisters()
        {
            result = controller.Register(model);
        }

        protected NewAccountModel model;
        protected RegistrationController controller;
        protected ActionResult result;
        protected IDocumentService session;
        protected IAuthCookieGenerator cookieGenerator;
    }

    [TestFixture]
    public class NewUser_Valid : NewUser
    {
        [Test]
        public void register()
        {
            new_user.WithScenario("valid registration")
                .Given(TheName, "Am?$#ory Blaine")
                .And(TheEmailAddress, "amory@blaine.me")
                .And(TheTimeZone, TimeZoneInfo.GetSystemTimeZones().First().Id)
                .And(ThePassword, "waaaah")
                .When(TheUserRegisters)
                .Then(TheNewAccountShouldBeCreated)
                .And(TheNameSlugShouldBeCorrectlyCalculated)
                .And(TheUserShouldBeAuthenticated).ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        private void TheUserShouldBeAuthenticated()
        {
            cookieGenerator.Received().SetAuthCookie(Arg.Any<string>());
        }

        private void TheNewAccountShouldBeCreated()
        {
            session.Received().Save(Arg.Any<Account>());
        }

        private void TheNameSlugShouldBeCorrectlyCalculated()
        {
            session.Received().Save(Arg.Is<Account>(account => account.NameSlug == "Am%3f%24%23ory-Blaine"));
        }
    }
}
