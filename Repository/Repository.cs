using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    /// <summary>
    /// Defines generic repository CRUD behaviors
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDataContext _context;

        public Repository(IDataContext context)
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

        public TEntity GetById(string id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Update(string id, TEntity entity)
        {
            var foundEntity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Entry(foundEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            //_context.Set<TEntity>().Update(entity);
        }

        public void Delete(string id)
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
    }
}
