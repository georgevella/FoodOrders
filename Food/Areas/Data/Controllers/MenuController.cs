using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;
using GSoft.Framework.IO;
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

        public ActionResult UploadCsv()
        {
            return PartialView("_CsvFileUploadView");
        }

        [HttpPost]
        public async Task<ActionResult> UploadCsv(FormCollection collection)
        {
            var r = new List<UploadFilesResult>();

            //Thread.Sleep(30*1000);

            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                await Task.Run(() =>
                {
                    var engine = new DelimeterSeperatedFile<CsvMenuEntry>();
                    var entries = engine.Read(new StreamReader(hpf.InputStream));

                    var store = GetRepositoryFor<Store>().First();
                    var menuItems = GetRepositoryFor<MenuItem>();
                    using (var tx = DataAccessLayer().BeginTransaction())
                    {
                        foreach (var entry in entries)
                        {
                            menuItems.Insert(new MenuItem()
                            {
                                Name = entry.Name,
                                Price = entry.Price,
                                CanHaveExtras = true,
                                Store = store
                            });
                        }

                        tx.Commit();
                    }
                });



                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }
            return Json(r);
        }
    }

    public class UploadFilesResult
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
    }

    public class CsvMenuEntry
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }



}
