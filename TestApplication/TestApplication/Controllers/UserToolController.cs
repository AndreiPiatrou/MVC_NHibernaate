#region [Imports]

using System;
using System.Web.Mvc;

using TestApplication.Models;
using TestApplication.WebServices;

#endregion

namespace TestApplication.Controllers
{
    public class UserToolController : Controller
    {
        private UserToolService service = new UserToolService();

        public ActionResult All()
        {
            ViewBag.UserTools = service.GetAll();
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(id > 0 ? service.GetByIdEntity(id) : new UserTool(-1, DateTime.Now));
        }
    }
}
