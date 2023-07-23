namespace CommerceClone.Interfaces
{
    public interface IRepository < TEntity >
    {
        TEntity Add (TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        ICollection<TEntity> GetAll();
        TEntity GetById(string id);
        void Update(string id, TEntity entity);
        void Delete(string id);
    }
}
