using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;

namespace FoodOrder.Areas.Data.Controllers
{
    public class ExtrasController : BaseDataController
    {
        public ExtrasController(IDataAccessLayer dataAccessLayer) : base(dataAccessLayer)
        {            
        }

        // GET: Data/Extras
        public ActionResult Index()
        {
            return View(GetRepositoryFor<Extras>());
        }

        // GET: Data/Extras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Data/Extras/Create
        public ActionResult Create()
        {
            ViewBag.Stores = GetRepositoryFor<Store>();
            return View();
        }

        // POST: Data/Extras/Create
        [HttpPost]
        public ActionResult Create(Extras extraItem, int store)
        {
            ViewBag.Stores = GetRepositoryFor<Store>();

            try
            {
                using (var tx = DataAccessLayer().BeginTransaction())
                {
                    var stores = GetRepositoryFor<Store>();
                    var extras = GetRepositoryFor<Extras>();

                    var storeInstance = stores.Get(store);
                    //extraItem.Store = storeInstance;
                    storeInstance.Extras.Add(extraItem);
                    extras.Insert(extraItem);

                    tx.Commit();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                
            }

            ViewBag.Stores = GetRepositoryFor<Store>();
            return View();
        }

        // GET: Data/Extras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Data/Extras/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Data/Extras/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Data/Extras/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
