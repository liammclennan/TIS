using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TodayIShall.Web.Controllers;
using TodayIShall.Web.Models;

namespace TodayIShall.Test.Registration
{
    [TestFixture]
    public class SignInModelTests
    {
        [Test]
        public void NameSlug()
        {
            var model = new SignInModel { Password = "qwerty", ReturnUrl = "/Today/Mark-Nopfler" };
            Assert.AreEqual("Mark-Nopfler", model.NameSlug);
        }

        [Test]
        public void NameSlug_TrailingSlash()
        {
            var model = new SignInModel { Password = "qwerty", ReturnUrl = "/Today/Mark-Nopfler/" };
            Assert.AreEqual("Mark-Nopfler", model.NameSlug);
        }

        [Test]
        public void ValidModel()
        {
            var model = new SignInModel {Password="qwerty", ReturnUrl = "/Today/Mark-Nopfler"};
            var validationResults = new List<ValidationResult>();
            Assert.IsTrue(IsValid(model, validationResults));
            Assert.AreEqual(0, validationResults.Count);
        }

        [Test]
        public void InvalidReturnUrl_MissingAccount()
        {
            var model = new SignInModel { Password = "qwerty", ReturnUrl = "/Today" };
            var validationResults = new List<ValidationResult>();
            Assert.IsTrue(IsValid(model, validationResults));
            Assert.AreEqual(0, validationResults.Count);
        }

        [Test]
        public void Invalid_NullPassword()
        {
            var model = new SignInModel { ReturnUrl = "/Today/blah" };
            var validationResults = new List<ValidationResult>();
            Assert.IsFalse(IsValid(model, validationResults));
            Assert.AreEqual("Password", validationResults[0].MemberNames.First());
        }

        [Test]
        public void Invalid_NullReturnUrl()
        {
            var model = new SignInModel { Password = "open" };
            var validationResults = new List<ValidationResult>();
            Assert.IsFalse(IsValid(model, validationResults));
            Assert.AreEqual("ReturnUrl", validationResults[0].MemberNames.First());
        }

        [Test]
        public void ValidModel_ExtraTrailingSlash()
        {
            var model = new SignInModel { Password = "qwerty", ReturnUrl = "/Today/hanschristienanderson/" };
            var validationResults = new List<ValidationResult>();
            Assert.IsTrue(IsValid(model, validationResults));
            Assert.AreEqual(0, validationResults.Count);
        }

        private bool IsValid(SignInModel model, List<ValidationResult> results)
        {
            var validationContext = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, validationContext, results);
        }
    }
}
