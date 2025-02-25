using PS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Data.Entities;

namespace PS.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByOffsetAsync(int offset, int count);

        Task<T> GetByIdAsync(int id);

        Task<bool> ContainsAsync(string name);        

        void Insert(T entity);

        void Update(T entity, string excludeProperties = "");

        void ActivateDeactivate(T entity);

        void ActivateDeactivate(int id);

        void Delete(T entity);

        void Delete(int id);

    }
}
