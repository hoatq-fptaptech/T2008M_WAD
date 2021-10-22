using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2008M_WAD.Models;
using System.IO;
namespace T2008M_WAD.Controllers
{
    public class CategoryController : Controller
    {
        private DataContext context = new DataContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(context.Categories.ToList());
        }

        [Authorize] // phai dang nhap vao thi moi xem dc
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id,Name")]Category category,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                // upload image
                string catImg = "~/Uploads/default.png"; //edit -  category.Image
                try
                {
                    if (Image != null)
                    {
                        string fileName = Path.GetFileName(Image.FileName);// lay url path khi upload len
                        string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        Image.SaveAs(path);
                        catImg = "~/Uploads/" + fileName;
                    }

                }
                catch (Exception e) {
                }
                finally
                {
                    category.Image = catImg;// set giá trị sau khi upload ảnh lên vào category
                }
                
                context.Categories.Add(category);// them 1 object vao list
                context.SaveChanges();// luu vao db
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,Name")]Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified; // bao rang day la trang thai chinh sua
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}