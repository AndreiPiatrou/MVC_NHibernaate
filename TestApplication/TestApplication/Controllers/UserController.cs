#region [Imports]

using System.Linq;
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

        public User CurrentUser { get; set; }

        /// <summary>
        ///     Show user list.
        /// </summary>
        /// <returns>User list.</returns>
        public ActionResult UserList()
        {
            var allUsers = service.GetAll();
            var model = new UserEditorModel { Users = allUsers, CurrentUser = allUsers.FirstOrDefault() };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = id > 0 ? service.GetByIdEntity(id) : new User { Id = id };
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User usertosave)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", usertosave);
            }

            service.SaveEntity(usertosave);
            return RedirectToAction("UserList");
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public ActionResult Save(User usertosave)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", usertosave);
            }

            service.SaveEntity(usertosave);
            return RedirectToAction("UserList");
        }
    }
}