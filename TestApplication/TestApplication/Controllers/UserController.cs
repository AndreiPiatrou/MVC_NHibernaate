#region [Imports]

using System.Web.Mvc;

using TestApplication.Models;
using TestApplication.WebServices;

#endregion

namespace TestApplication.Controllers
{
    /// <summary>
    ///     Controller for users.
    /// </summary>
    public class UserController : Controller
    {
        private readonly UserService service = new UserService();

        /// <summary>
        ///     Show user list.
        /// </summary>
        /// <returns>User list.</returns>
        public ActionResult UserList()
        {
            ViewBag.Users = service.GetAll();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var user = id > 0 ? service.GetByIdEntity(id) : new User { Id = id };
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public ActionResult Save(User usertosave)
        {
            service.SaveEntity(usertosave);
            return RedirectToAction("UserList");
        }
    }
}