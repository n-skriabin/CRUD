using CRUD.DataAccess;
using CRUD.Domain;
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
    public class ArticlesController : Controller
    {
        ArticlesService articlesService;

        public ArticlesController()
        {
            articlesService = new ArticlesService(WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].Name);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Articles";

            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(articlesService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Create(ArticleViewModel articleViewModel)
        {
            articlesService.Create(articleViewModel);
            return Json(new[] { articleViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Update(ArticleViewModel articleViewModel)
        {
            articlesService.Update(articleViewModel);
            return Json(new[] { articleViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Delete(ArticleViewModel articleViewModel)
        {
            articlesService.Delete(articleViewModel);
            return Json(new[] { articleViewModel });
        }

        public ActionResult ReadArticlesForMultiSelect(ArticleViewModel articleViewModel)
        {
            return Json(articlesService.GetArticlesForView(), JsonRequestBehavior.AllowGet);
        }
    }
}