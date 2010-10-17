using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Queries.AccountQueries;
using TodayIShall.Web.Models;

namespace TodayIShall.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IDocumentService _documentService;

        public AuthController(IDocumentService _documentService)
        {
            this._documentService = _documentService;
        }

        //
        // GET: /Auth/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignIn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(SignInModel model)
        {
            var account = _documentService.Query(new AccountByNameSlug(model.NameSlug)).FirstOrDefault();
            if (account==null) return RedirectToAction("Index", "Registration");
            if (account.IsCorrectPassword(model.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(account.NameSlug, true);
                return Content(""); // required
            }
            return RedirectToAction("SignIn");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Registration");
        }
    }
}
