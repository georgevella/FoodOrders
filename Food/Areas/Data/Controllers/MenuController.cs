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
    public class MenuController : BaseDataController
    {
        public MenuController(IDataAccessLayer dal) : base(dal)
        {

        }
 
        public ActionResult Index()
        {
            var menuItems = GetRepositoryFor<MenuItem>();            
            return View(menuItems);
        }

        public ActionResult View(int? id)
        {
            var menuItems = GetRepositoryFor<MenuItem>();

            if (id.HasValue)
            {
                var stores = GetRepositoryFor<Store>();
                var store = stores.Get(id);
                ViewBag.Store = store;      // save the store associated with the menu we're viewing
                return View("Index", menuItems.Where(m => m.Store == store));
            }

            ViewBag.Store = null;       
            return View("Index", menuItems);
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

            return View();
        }

        // GET: Admin/MenuItem/Edit/5
        public ActionResult Edit(int id)
        {
            var menus = GetRepositoryFor<MenuItem>();
            return View(menus.Get(id));
        }

        // POST: Admin/MenuItem/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuItem menuItem, int store)
        {
            try
            {
                using (var tx = DataAccessLayer().BeginTransaction())
                {
                    var stores = GetRepositoryFor<Store>();
                    var menus = GetRepositoryFor<MenuItem>();

                    var storeInstance = stores.Get(store);                    
                    menuItem.Store = storeInstance;
                    menus.Update(menuItem);

                    tx.Commit();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Admin/MenuItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var tx = DataAccessLayer().BeginTransaction())
                {
                    var menus = GetRepositoryFor<MenuItem>();
                    
                    menus.Delete(menus.Get(id));                    
                    tx.Commit();
                }

                //return RedirectToAction("Index");
                return Json(id);
            }
            catch
            {
                return View();
            }
        }
    }
}
