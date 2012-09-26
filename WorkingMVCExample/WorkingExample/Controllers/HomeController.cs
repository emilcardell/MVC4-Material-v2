using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using WorkingExample.Models;

namespace WorkingExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession _documentSession;

        public HomeController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public ActionResult Index()
        {
            return View(_documentSession.Query<Product>().ToList());
        }

        public ActionResult ByTag(string tagName)
        {
            ViewBag.tagName = tagName;
            return View("Index", _documentSession.Query<Product>().Where(x => x.Tags.Any(y => y == tagName)).ToList());
        }

    }
}
