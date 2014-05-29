using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;

namespace FoodOrder.Areas.Data.Controllers
{
    public class StoreController : BaseDataController
    {
        public StoreController(IDataAccessLayer dal) : base(dal)
        {
        }

        // GET: Admin/Store
        public ActionResult Index()
        {
            return View(GetRepositoryFor<Store>());
        }

        // GET: Admin/Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Store/Create
        [HttpPost]
        public ActionResult Create(Store newStore)
        {
            try
            {
                using (var tx = DataAccessLayer().BeginTransaction())
                {
                    var stores = DataAccessLayer().GetRepositoryFor<Store>();
                    stores.Insert(newStore);

                    tx.Commit();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Store/Edit/5
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

        // GET: Admin/Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Store/Delete/5
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
