using System.Web.Mvc;

using TestApplication.Models;
using TestApplication.WebServices;

namespace TestApplication.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentService service = new DepartmentService();

        public ActionResult All()
        {
            return View(service.GetAll());
        }

        public ActionResult Edit(int id)
        {
            return View(id > 0 ? service.GetByIdEntity(id) : new Department { Id = id });
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("All");
        }

        public ActionResult Save(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", department);
            }

            service.SaveEntity(department);
            return RedirectToAction("All");
        }
    }
}
