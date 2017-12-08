using CRUD.Services;
using CRUD.Views;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using CRUD.Views.ResponseModels;

namespace CRUD.Web.Controllers
{
    public class BooksController : Controller
    {
        BooksService booksService;

        public BooksController()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            booksService = new BooksService(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var books = booksService.Read();
            if(books == null)
            {
                return null;
            }
            return Json(books.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Create(ResponseBookViewModel responseBookViewModel)
        {
            var bookViewModel = booksService.Create(responseBookViewModel);
            return Json(new[] { bookViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Update(ResponseBookViewModel responseBookViewModel)
        {
            var bookViewModel = booksService.Update(responseBookViewModel);
            return Json(new[] { bookViewModel });
        }

        [AcceptVerbs(System.Web.Mvc.HttpVerbs.Post)]
        public ActionResult Delete(BookViewModel bookViewModel)
        {
            booksService.Delete(bookViewModel);
            return Json(new[] { bookViewModel });
        }

        public ActionResult ReadBooksForMultiselect(BookViewModel bookViewModel)
        {
            return Json(booksService.Read(), JsonRequestBehavior.AllowGet);
        }
    }
}