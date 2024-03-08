using lezione70z.Models;
using System.Web.Mvc;

namespace lezione70z.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.IsAuthenticated = GetIsAuthenticated();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (DataAccess.IsHostAuthenticated(model.Nome, model.Cognome, model.Username, model.Password))
                {
                   
                    SetIsAuthenticated(true);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Nome, Cognome, Username o Password non validi.");
            }

            SetIsAuthenticated(false);
            TempData["LoginModel"] = model;
            return View("Index");
        }

        private bool GetIsAuthenticated()
        {
            return Session["IsAuthenticated"] as bool? ?? false;
        }

        private void SetIsAuthenticated(bool isAuthenticated)
        {
            Session["IsAuthenticated"] = isAuthenticated;
        }
    }
}
