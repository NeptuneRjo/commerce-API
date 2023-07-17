using CommerceClone.Interfaces;

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

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
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

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Update(int id, TEntity entity)
        {
            var foundEntity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Entry(foundEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            //_context.Set<TEntity>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
