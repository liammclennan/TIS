using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Norm;
using TodayIShall.Core.AppServices;
using TodayIShall.Core.Domain;
using TodayIShall.Core.Queries.AccountQueries;
using TodayIShall.Web.Infrastructure;
using TodayIShall.Web.Models;

namespace TodayIShall.Web.Controllers
{
    [IdentityAuthorize]
    public class TodayController : Controller
    {
        private readonly IDocumentService _documentService;

        public TodayController(IDocumentService _documentService)
        {
            this._documentService = _documentService;
        }

        public ActionResult Index(string nameslug)
        {
            var account = _documentService.Query(new AccountByNameSlug(nameslug)).First();
            var model = new TodayModel();
            model.BindTo(account);
            return View("Index", model);
        }

        public ActionResult CopyForward(ForwardBackModel model)
        {
            var account = _documentService.Query(new AccountByNameSlug(model.nameslug)).First();
            account.CopyForward(model.AsDay());
            _documentService.Save(account);
            return RedirectToAction("Index");
        }

        public ActionResult BackADay(ForwardBackModel inModel)
        {
            var account = _documentService.Query(new AccountByNameSlug(inModel.nameslug)).First();
            var model = new TodayModel();
            model.BindTo(account, inModel.AsDay().AddDays(-1));
            return View("Index", model);
        }

        public ActionResult ForwardADay(ForwardBackModel inModel)
        {
            var account = _documentService.Query(new AccountByNameSlug(inModel.nameslug)).First();
            var model = new TodayModel();
            model.BindTo(account, inModel.AsDay().AddDays(1));
            return View("Index", model);
        }

        public ActionResult AddGoal(AddRemoveGoalModel model)
        {
            var account = _documentService.Query(new AccountByNameSlug(model.NameSlug)).First();
            account.AddGoal(model.goal, model.CalendarDay); 
            _documentService.Save(account);
            return Content("");
        }

        public ActionResult RemoveGoal(string NameSlug, Guid Id)
        {
            var account = _documentService.Query(new AccountByNameSlug(NameSlug)).First();
            account.RemoveGoal(Id);
            _documentService.Save(account);
            return Content(""); 
        }

        public ActionResult Done(string NameSlug, Guid Id)
        {
            var account = _documentService.Query(new AccountByNameSlug(NameSlug)).First();
            account.Done(Id);
            _documentService.Save(account);
            return Content("");
        }

        public ActionResult Undone(string NameSlug, Guid Id)
        {
            var account = _documentService.Query(new AccountByNameSlug(NameSlug)).First();
            account.Undone(Id);
            _documentService.Save(account);
            return Content("");
        }

    }
}
