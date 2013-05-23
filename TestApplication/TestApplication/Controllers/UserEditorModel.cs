using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class UserEditorModel
    {
        public PagedList.IPagedList<User> Users { get; set; }

        public User CurrentUser { get; set; }

        public int NewCurrentUserId { get; set; }
    }
}