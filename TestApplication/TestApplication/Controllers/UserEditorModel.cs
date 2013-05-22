using System.Collections.Generic;

using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class UserEditorModel
    {
        public List<User> Users { get; set; }

        public User CurrentUser { get; set; }

        public int NewCurrentUserId { get; set; }
    }
}