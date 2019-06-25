using Demo.DDD.Shared.Entitys;

namespace Demo.DDD.Domain.Repositories
{
    public interface IRepository<T> where T: IAggregateRoot
    {
    }
}
