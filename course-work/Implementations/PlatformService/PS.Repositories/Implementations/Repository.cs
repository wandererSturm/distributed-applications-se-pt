using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Data.Entities;

namespace PS.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public Repository(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context), "An instance of DbContext is required to use this repository.");
            this.DbSet = this.Context.Set<T>();
        }

        public virtual void ActivateDeactivate(T entity)
        {            
            this.Update(entity);
        }

        public virtual void ActivateDeactivate(int id)
        {
            var entity = this.DbSet.Find(id);
            if (entity != null)
            {
                ActivateDeactivate(entity);
            }
        }

        public void Delete(T entity)
        {
            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = this.GetByIdAsync(id).Result;
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await SoftDeleteQueryFilter(this.DbSet.AsQueryable(), -1).ToListAsync();

#pragma warning disable CS8603 // Possible null reference return.
        public async Task<T> GetByIdAsync(int id) => await this.DbSet.FindAsync(id);        
#pragma warning restore CS8603 // Possible null reference return.

        public void Insert(T entity)
        {

            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.AddAsync(entity);
            }
        }

        public void Update(T entity, string excludeProperties = "")
        {

            EntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;

            entry.Property("CreatedBy").IsModified = false;
            entry.Property("CreatedOn").IsModified = false;

            foreach (var excludeProperty in excludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                entry.Property(excludeProperty).IsModified = false;
            }

        }

        public virtual void Save(T entity)
        {
            if (entity.Id == 0)
            {
                this.Insert(entity);
            }
            else
            {
                this.Update(entity);
            }
        }

        protected static IQueryable<T> SoftDeleteQueryFilter(IQueryable<T> query, int id)
        {
            return query;
            if (id != -1)
            {
                query = query.Where(x => x.Id == id);
            }
            return query;
        }

        public async Task<bool> ContainsAsync(string name)
        {
            return await this.DbSet.Where(i => i.Name == name).FirstOrDefaultAsync() != null;
        }

        public async Task<(int, IEnumerable<T>)> GetByOffsetAsync(int offset, int count)
        {
            int total = this.DbSet.Count();
            return (total, await this.DbSet.Skip(offset).Take(count).OrderByDescending(a => a.Id).AsQueryable().ToListAsync());            
        }
    }
}
