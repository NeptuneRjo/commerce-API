namespace CommerceClone.Interfaces
{
    public interface IRepository < TEntity >
    {
        void Add (TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        ICollection<TEntity> GetAll();
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
