using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrder.DataAccess;
using FoodOrder.DataAccess.Model;
using NHibernate.Tool.hbm2ddl;

namespace DbMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var dataDirectory = Path.Combine(currentDirectory, "data");

            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
            Console.WriteLine("DataDirectory: {0}", dataDirectory);

            var factory = new DataAccessLayerFactory();
            factory.Initialize();

            using (var dal = factory.Open())
            {
                var store = dal.GetRepositoryFor<Store>();

                store.Insert(new Store()
                {
                    Name = "Mercury",
                    Menu =
                    {
                        new MenuItem() {Name = "Tuna Salad", Price = 350},
                        new MenuItem() {Name = "Chicken Salad", Price = 400},
                        new MenuItem() {Name = "Green Salad", Price = 200},
                        new MenuItem() {Name = "English Breakfast", Price = 300},
                    }
                });

                dal.Commit();
            }

            using (var dal = factory.Open())
            {
                var users = dal.GetRepositoryFor<User>();

                users.Insert(new User()
                {
                    DisplayName = "Test User",
                    UserName = "testuser",
                    EmailAddress = "test@user.mail",
                    PasswordHash = "testuserpass",
                    PhoneNumber = "2205"
                });

                users.Insert(new User()
                {
                    DisplayName = "Test User 2",
                    UserName = "testuser2",
                    EmailAddress = "test2@user.mail",
                    PasswordHash = "testuserpass",
                    PhoneNumber = "2206"
                });

                dal.Commit();

            }

            Console.WriteLine("Press any key to start test");
            Console.ReadKey();

            Console.WriteLine("Add order");
            using (var dal = factory.Open())
            {
                var stores = dal.GetRepositoryFor<Store>();
                var users = dal.GetRepositoryFor<User>();
                var orders = dal.GetRepositoryFor<Order>();

                var mercury = stores.First();
                var user = users.First(x => x.UserName.Equals("testuser"));

                var order = user.CreateOrderFrom(mercury);                

                orders.Insert(order);
                users.Update(user);

                dal.Commit();
            }

            Console.WriteLine("Add order item");
            using (var dal = factory.Open())
            {
                var users = dal.GetRepositoryFor<User>();
                var menu = dal.GetRepositoryFor<MenuItem>();
                var orders = dal.GetRepositoryFor<Order>();
                var orderItems = dal.GetRepositoryFor<OrderItem>();

                var cuser = users.First(x => x.UserName.Equals("testuser2"));
                var order = orders.First();
                var menuItem = menu.Get(m => m.Name.Equals("Tuna Salad"));

                var item = order.AddItem(menuItem, cuser);

                orders.Update(order);
                users.Update(cuser);
                orderItems.Insert(item);


                dal.Commit();
            }

            Console.WriteLine("Getting menu for store");
            using (var dal = factory.Open())
            {
                var stores = dal.GetRepositoryFor<Store>();
                foreach (var item in stores.First().Menu)
                {
                    Console.WriteLine("{0}", item.Name);
                }
            }

            Console.WriteLine("Getting order items");
            using (var dal = factory.Open())
            {
                var orders = dal.GetRepositoryFor<Order>();
                var order = orders.First();

                foreach (var item in order.Items)
                    Console.WriteLine("{0} [{1}]", item.Item.Name, item.OrderedBy.UserName);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
