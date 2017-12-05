using CRUD.Services;
using CRUD.Views;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace CRUD.Web.Controllers
{
    public class JournalsController : Controller
    {
        JournalsService journalsService;

        public JournalsController()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].Name;
            journalsService = new JournalsService(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(journalsService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Create(ResponseJournalViewModel responseJournalViewModel)
        {          
            var journalViewModel = journalsService.Create(responseJournalViewModel);
            return Json(new[] { journalViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Update(ResponseJournalViewModel responseJournalViewModel)
        {
            var journalViewModel = journalsService.Update(responseJournalViewModel);
            return Json(new[] { journalViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Delete(JournalViewModel journalViewModel)
        {
            journalsService.Delete(journalViewModel);
            return Json(new[] { journalViewModel });
        }

        public ActionResult ReadJournalsForMultiselect(JournalViewModel journalViewModel)
        {
            return Json(journalsService.Read(), JsonRequestBehavior.AllowGet);
        }
    }
}