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
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            articlesService = new ArticlesService(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var articles = articlesService.Read();
            if (articles == null)
            {
                return null;
            }
            return Json(articles.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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