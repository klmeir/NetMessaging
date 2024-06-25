using NetMessaging.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace NetMessaging.Infrastructure.Ports
{
    public interface IRepository<T> where T : DomainEntity
    {
        Task<T> AddAsync(T entity);

    }
}
