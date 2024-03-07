using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess;

public interface IEntityRepository<T> where T : class, IEntity, new() //IEntityden implement olan classları al
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    List<T> GetList(Expression<Func<T,bool>> filter = null); // Sorgu ile çağırmak için ekledik
    T Get(Expression<Func<T, bool>> filter); // Sorgu ile çağırmak için ekledik
}
