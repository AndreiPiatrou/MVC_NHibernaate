#region [Imports]

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using NHibernate.Criterion;

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
        private readonly DepartmentService departmentService = new DepartmentService();

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
            var departmentsList = new DepartmentService().GetAll().Select(department => new SelectListItem() { Value = department.Id.ToString(CultureInfo.InvariantCulture), Text = department.Name }).ToList();

            ViewBag.Departments = departmentsList;
            user.DepartmentId = user.Department.Id;
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
                var departmentsList = departmentService.GetAll().Select(department => new SelectListItem() { Value = department.Id.ToString(CultureInfo.InvariantCulture), Text = department.Name }).ToList();
                ViewBag.Departments = departmentsList;
                return View("Edit", usertosave);
            }

            usertosave.Department = departmentService.GetByIdEntity(usertosave.DepartmentId);
            service.SaveEntity(usertosave);
            return RedirectToAction("UserList");
        }

        public ActionResult GetTools()
        {
            System.Threading.Thread.Sleep(2000);
            var toolsService = new UserToolService();
            var tools = toolsService.GetAll();
            ViewBag.Tools = tools;
            return PartialView();
        }

        public ActionResult SearchUser(string term)
        {
            var formattedname = string.Format("%{0}%", term.ToLower());
            var results =
                service.FindByexpression(
                    user => user.FirstName.IsLike(formattedname) || user.LastName.IsLike(formattedname));

            var jsonResult = from user in results
                             select
                                 new
                                 {
                                     id = user.Id,
                                     label = string.Format("{0} {1}", user.FirstName, user.LastName),
                                     value = string.Format("{0} {1}", user.FirstName, user.LastName)
                                 };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}