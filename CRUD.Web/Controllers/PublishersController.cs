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
using CRUD.Views.ResponseModels;

namespace CRUD.Web.Controllers
{
    public class PublishersController : Controller
    {
        PublishersService publishersService;

        public PublishersController()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].Name;
            publishersService = new PublishersService(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(publishersService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Create(ResponsePublisherViewModel responsePublisherViewModel)
        {
            var publisherViewModel = publishersService.Create(responsePublisherViewModel);
            return Json(new[] { publisherViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Update(ResponsePublisherViewModel responsePublisherViewModel)
        {
            var publisherViewModel = publishersService.Update(responsePublisherViewModel);
            return Json(new[] { publisherViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Delete(PublisherViewModel publisherViewModel)
        {
            publishersService.Delete(publisherViewModel);
            return Json(new[] { publisherViewModel });
        }
    }
}