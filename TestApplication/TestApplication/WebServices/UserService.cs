#region [Imports]

using TestApplication.Models;

#endregion

namespace TestApplication.WebServices
{
    public class UserService : NHibernateService<User, int>
    {
    }
}