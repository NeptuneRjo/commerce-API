namespace CommerceClone.Interfaces
{
    public interface IRepository < TEntity >
    {
        TEntity Add (TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        ICollection<TEntity> GetAll();
        TEntity GetById(int id);
        void Update(int id, TEntity entity);
        void Delete(int id);
        void SaveChanges();
        /// <summary>
        /// Maps the entity to the type provided, 
        /// given there is a matching profile.
        /// <br/>
        /// Can map a collection, provided the type is a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>The mapped entity</returns>
        T Map<T>(dynamic entity);
    }
}
