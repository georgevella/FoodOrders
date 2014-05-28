using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FluentNHibernate.Utils;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;
using MenuItem = FoodOrder.DataAccess.Model.MenuItem;

namespace FoodOrder.Areas.Data.Controllers
{
    public class MenuItemsController : BaseDataController
    {
        public MenuItemsController(IDataAccessLayer dal) : base(dal)
        {

        }
        // GET: Admin/MenuItem
        public ActionResult Index()
        {
            return View(GetRepositoryFor<MenuItem>());
        }

        // GET: Admin/MenuItem/Details/5
        public ActionResult Details(int id)
        {
            var menus = GetRepositoryFor<MenuItem>();
            return View(menus.Get(id));
        }

        // GET: Admin/MenuItem/Create
        public ActionResult Create()
        {
            ViewBag.Stores = GetRepositoryFor<Store>();
            return View();
        }

        // POST: Admin/MenuItem/Create
        [HttpPost]
        //public ActionResult Create(MenuItem newitem)
        public ActionResult Create(MenuItem menuItem, int store)
        {
            try
            {
                using (var tx = DataAccessLayer().BeginTransaction())
                {
                    var stores = GetRepositoryFor<Store>();
                    var menus = GetRepositoryFor<MenuItem>();

                    var storeInstance = stores.Get(store);
                    storeInstance.Menu.Add(menuItem);
                    //menuItem.Store = storeInstance;
                    menus.Insert(menuItem);

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

        // GET: Admin/MenuItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/MenuItem/Edit/5
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

        // GET: Admin/MenuItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/MenuItem/Delete/5
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
