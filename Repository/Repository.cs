using AutoMapper;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Repository
{
    /// <summary>
    /// Defines generic repository CRUD behaviors
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public Repository(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
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

        public T Map<T>(dynamic entity)
        {
            return _mapper.Map<T>(entity);
        }
    }
}
