using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDash.Models;

namespace WebDash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entries = Histograms.Instance.GetValues().OrderByDescending(x => x.Value).ToList();
            return View(entries);
        }
    }
}