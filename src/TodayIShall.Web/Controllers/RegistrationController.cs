using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Domain;
using TodayIShall.Web.Models;

namespace TodayIShall.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IAuthCookieGenerator _authCookieGenerator;

        public RegistrationController(IDocumentService documentService, IAuthCookieGenerator authCookieGenerator)
        {
            _documentService = documentService;
            _authCookieGenerator = authCookieGenerator;
        }

        public ActionResult Index()
        {
            return View(new NewAccountModel());
        }

        public ActionResult Register(NewAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var account = new Account();
            model.Update(account);
            _documentService.Save(account);

            _authCookieGenerator.SetAuthCookie(account.NameSlug);

            return RedirectToAction("Index", "Today", new {nameslug=account.NameSlug});
        }
    }

    public interface IAuthCookieGenerator
    {
        void SetAuthCookie(string nameSlug);
    }

    public class AuthCookieGenerator : IAuthCookieGenerator
    {
        public void SetAuthCookie(string nameSlug)
        {
            FormsAuthentication.SetAuthCookie(nameSlug, true);
        }
    }
}
