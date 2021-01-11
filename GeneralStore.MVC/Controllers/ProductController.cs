using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        // Add the application DB context (link to the database)
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Product INDEX Method?
        public ActionResult Index()
        {
            // later we will return an orderewd list of products beore the final view is returned
            List<Product> productList = _db.Products.ToList();
            List<Product> orderedList = productList.OrderBy(prod => prod.Name).ToList();
            // see below (modifying ApplicationDbContext class)
            return View(_db.Products.ToList());
            //  this results in a list ordered by product name.  You can choose another field to sort by, and the OrderBy method will take care of it. prod.Price for example (dot operator)
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(); // right click here to make the view (in the GET sections)
        }

        // POST: Product
        [HttpPost]
        //[ValidateAntiForgeryToken] ??
        public ActionResult Create(Product product) // should this be better named "CreateProduct" for clairity?
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);   // why are we returning to product view if model is not valid?

            /* NOTES::
             * When a View is returned (as with return View...), this tells MVC to generate HTML to be displayed and sends it to the browser. 
             * On the other hand, a call to RedirectToAction tells ASP.NET MVC to respond with a Browser redirect to a different action instead of rendering HTML. 
             * So, here, if we successfully add the new product, then we should return to the Index view.
             * If we have not successfully added the product, then we should leave it as is, and maybe the user will make modification to enable it to save.
            */
        }

        // GET: Delete
        // Product/Delete/{id}
        public ActionResult Delete(int? id) // int? is an alias for the Nullable<int> struct, which you can set to null.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Delete
        // Product/Delete/{id}
        [HttpPost, ActionName("Delete")] // this is different from the rater app, just an [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Edit
        // Product/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Edit
        // Product/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Details
        // Product/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}