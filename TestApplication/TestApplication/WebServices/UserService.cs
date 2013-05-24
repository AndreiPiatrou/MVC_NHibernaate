#region [Imports]

using System.Collections.Generic;

using NHibernate.Criterion;

using TestApplication.Models;

#endregion

namespace TestApplication.WebServices
{
    public class UserService : NHibernateService<User, int>
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="namePart"></param>
        /// <returns></returns>
        public List<User> FindByString(string namePart)
        {
            namePart = string.Format("%{0}%", namePart.ToLower());
            return FindByexpression(user => user.FirstName.IsLike(namePart) || user.LastName.IsLike(namePart));
        }
    }
}