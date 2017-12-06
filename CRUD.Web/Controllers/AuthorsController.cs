using AutoMapper;
using CRUD.DataAccess;
using CRUD.Services;
using CRUD.Views;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CRUD.Web.Controllers
{
    public class AuthorsController : Controller
    {
        AuthorsService authorsService;

        public AuthorsController()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].Name;
            authorsService = new AuthorsService(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(authorsService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Create(AuthorViewModel authorViewModel)
        {
            authorsService.Create(authorViewModel);
            return Json(new[] { authorViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Update(AuthorViewModel authorViewModel)
        {
            authorsService.Update(authorViewModel);
            return Json(new[] { authorViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Delete(AuthorViewModel authorViewModel)
        {
            authorsService.Delete(authorViewModel);
            return Json(new[] { authorViewModel });
        }

        public ActionResult ReadAuthorsForDropDown()
        {
            var authors = authorsService.Read();
            return Json(authors, JsonRequestBehavior.AllowGet);
        }
    }
}