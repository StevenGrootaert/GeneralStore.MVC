using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToList());  // add the DbSet to the IdenityModels.cs "this isn't how this should be done in your final project" ?!!
        }

        // GET: Transaction/{id}
        public ActionResult Details(int? id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create() // right click here to make the view that we'll be returning below (but we're using the CreateTransactionViewModel for the class)
        {
            var viewModel = new CreateTransactionViewModel(); // always need to pass in a new instance of the model

            viewModel.Customers = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });

            viewModel.Products = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });
            
            return View(viewModel);
            // return View(new Transaction());     // this just returns the view that we see when we enter the information to make a transaction
        }

        // two ways to make this... one (1) above with the view model (easier to maintain and easy to see whats required for every single view) 
        // and (2) another using ViewData / ViewBags which uses 
        //      the actual model but needs to be in the GET and the POST request. 
        //      ViewData["Customers"] = "";
        //      Seen in the edit method way below. 
        // we're joining ONE of our existing customers with ONE of our existing products - done via a drop down
        // we need a ViewModel becuas we can't fit it all in a single class

        // POST: Transaction/Create (same URL but a POST request to submit the info we entered)
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(Transaction)
        public ActionResult Create(CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //_db.Transactions.Add(viewModel); we have to make our own entity to add to Transactions DB
                // what happens in ModelView Controller you end up with code coming from a creat view model 
                _db.Transactions.Add(new Transaction
                {
                    CustomerId = viewModel.CustomerId,
                    ProductId = viewModel.ProductId
                });
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        
            return View(viewModel); // we're not getting the view model back with the drop downs - never solved?
        }

        // GET: Transaction/Delete/{id}
        public ActionResult Delete(int? id)
        {
            Transaction transaction = _db.Transactions.Find(id); // nick did this first - in canvas it wa second after verifying that id is not equal (!=) null
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Transaction transaction)
        {
            // do BD stuff ??
            //Transaction transaction = _db.Transactions.Find(id); -- this isn't working
            if (/* somthing goes wrong*/ 1 != 1)
            {
                // return custom view
                ViewData["ErrorMessage"] = "not able to delete the transaction";
                return View(transaction);
            }
            return RedirectToAction("Index");
        }

        // GET: Transaction/Edit/{id}  // **using ViewData to populate dropdowns -- need it in both views
        public ActionResult Edit(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });

            ViewData["Products"] = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(transaction);
        }

        // POST: Transaction/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });

            ViewData["Products"] = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
            // need to save to DB 
            return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}