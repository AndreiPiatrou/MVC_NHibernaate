using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate;
using NHibernate.Cfg;

namespace TestApplication.Models
{
    public class UserService
    {
        private ISessionFactory sessionFactory;

        public List<User> GetUsers()
        {
            using (ISession session = OpenSession())
            {
                return session.QueryOver<User>().List<User>().ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (ISession session = OpenSession())
            {
                return session.Get<User>(id);
            }
        }

        public int CreateOrSaveUser(User user)
        {
            ISession session = OpenSession();
            ITransaction tran = session.BeginTransaction();
            int empNo = 0;
            if (user.Id < 1)
            {
                empNo = (int)session.Save(user);
            }
            else
            {
                session.Update(user);
                empNo = user.Id;
            }

            tran.Commit();

            return empNo;
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            DeleteUser(user);
        }

        public void DeleteUser(User user)
        {
            ISession session = OpenSession();
            ITransaction tran = session.BeginTransaction();
            session.Delete(user);
            tran.Commit();
        }

        /// <summary>
        ///     Open n-hibernate session.
        /// </summary>
        /// <returns>Session.</returns>
        private ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                var cgf = new Configuration();
                var data = cgf.Configure(
                      HttpContext.Current.Server.MapPath(@"~\NHibernate\Configuration\hibernate.cfg.xml"));
                cgf.AddDirectory(new System.IO.DirectoryInfo(
                      HttpContext.Current.Server.MapPath(@"~\NHibernate\Mappings\")));
                sessionFactory = data.BuildSessionFactory();
            }

            return sessionFactory.OpenSession();
        }
    }
}