using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2008M_WAD.Models;
namespace T2008M_WAD.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Demo()
        {
            ViewData["xgame"] = 13;
            ViewBag.XGame = 26;
            var day = 13;
            return View(day);// compact data - chi truyen dc 1 object
        }

        public ActionResult ListCategory()
        {
            DataContext dc = new DataContext();
            List<Category> categories = dc.Categories.ToList();
           //categories.Add(new Category() { Name="Fashions" });
            return View(categories);
        }

        public ActionResult Form()
        {
            var c = new Category() { Name = "Fashions" };// neu co compact thi form se nap data vao input
            return View(c);
        }
    }
}