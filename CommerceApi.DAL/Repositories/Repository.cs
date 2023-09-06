using CommerceApi.DAL.Data;
using CommerceApi.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommerceApi.DAL.Repository
{
    /// <summary>
    /// Defines generic repository CRUD behaviors
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(predicate);
        }


        public ICollection<TEntity> GetAllByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate).ToList();
        }

        public void Update(int id, TEntity entity)
        {
            var foundEntity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Entry(foundEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public string GenerateUid(TEntity entity, string marker)
        {
            string id;

            do
            {
                id = Guid.NewGuid().ToString("N");
            } while (_context.Set<TEntity>().Find($"{marker.ToLower()}_{id}") != null);

            return id;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
