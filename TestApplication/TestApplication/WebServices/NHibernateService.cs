#region [Imports]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public abstract class NHibernateService<TEntity, TId> where TEntity : class, IEntity<TId>
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
        ///     GEt entities with offset.
        /// </summary>
        /// <param name="page">Current page offset.</param>
        /// <param name="countPerPage">Count per page.</param>
        /// <returns>Entities collection.</returns>
        public List<TEntity> GetEntitiesPerPage(int page, int countPerPage)
        {
            using (ISession session = OpenSession())
            {
                return session.QueryOver<TEntity>().Skip(page * countPerPage).Take(countPerPage).List<TEntity>().ToList();
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

        /// <summary>
        ///     Save entity.
        ///     For correct saving entity id must be of default or none.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        /// <returns>Saved entity with id.</returns>
        public TEntity SaveEntity(TEntity entity)
        {
            ISession session = OpenSession();
            ITransaction tran = session.BeginTransaction();

            if (entity.Id.Equals(default(TId)))
            {
                entity.Id = (TId)session.Save(entity);
            }
            else
            {
                session.Update(entity);
            }

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
        /// Search entities by expression.
        /// </summary>
        /// <param name="expression">Search expression.</param>
        /// <returns>Search results.</returns>
        public List<TEntity> FindByexpression(Expression<Func<TEntity, bool>> expression)
        {
            return OpenSession().QueryOver<TEntity>().Where(expression).List().ToList();
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