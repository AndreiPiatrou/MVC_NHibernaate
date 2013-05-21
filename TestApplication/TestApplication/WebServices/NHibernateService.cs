#region [Imports]

using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate;
using NHibernate.Cfg;

using TestApplication.Models;

#endregion

namespace TestApplication.WebServices
{
    /// <summary>
    ///     NHibernate service.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TId">Entity id.</typeparam>
    public class NHibernateService<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        /// <summary>
        ///     Session factory.
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        ///     Gets all entities.
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            using (ISession session = OpenSession())
            {
                return session.QueryOver<TEntity>().List<TEntity>().ToList();
            }
        }

        /// <summary>
        ///     Gets entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetByIdEntity(TId id)
        {
            using (ISession session = OpenSession())
            {
                return session.Get<TEntity>(id);
            }
        }

        public TEntity SaveEntity(TEntity entity)
        {
            ISession session = OpenSession();
            ITransaction tran = session.BeginTransaction();
            session.SaveOrUpdate(entity);
            tran.Commit();

            return entity;
        }

        /// <summary>
        ///     Delete entity by id.
        /// </summary>
        /// <param name="id">Entity id..</param>
        /// <returns>Delete result.</returns>
        public bool Delete(TId id)
        {
            var entity = GetByIdEntity(id);
            return Delete(entity);
        }

        /// <summary>
        ///     Delete entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <returns>If deleting is Ok.</returns>
        public bool Delete(TEntity entity)
        {
            ISession session = OpenSession();
            ITransaction tran = session.BeginTransaction();
            session.Delete(entity);
            tran.Commit();

            return true;
        }
        
        /// <summary>
        ///     Open n-hibernate session.
        /// </summary>
        /// <returns>Opened session.</returns>
        private ISession OpenSession()
        {
            if (sessionFactory != null)
            {
                return sessionFactory.OpenSession();
            }

            var cgf = new Configuration();
            var data = cgf.Configure(
                HttpContext.Current.Server.MapPath(@"~\NHibernate\Configuration\hibernate.cfg.xml"));
            cgf.AddDirectory(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(@"~\NHibernate\Mappings\")));
            sessionFactory = data.BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}