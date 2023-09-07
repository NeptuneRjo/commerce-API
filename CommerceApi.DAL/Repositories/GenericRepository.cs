using CommerceApi.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommerceApi.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<ICollection<TEntity>> AddRange(ICollection<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }


        public async Task<ICollection<TEntity>> GetAllByQuery(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            //TEntity foundEntity = await _context.Set<TEntity>().FindAsync(id);

            //_context.Set<TEntity>().Entry(foundEntity).CurrentValues.SetValues(entity);
            //await _context.SaveChangesAsync();

            //return foundEntity;
            _ = _context.Set<TEntity>().Update(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
