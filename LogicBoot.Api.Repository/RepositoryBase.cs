using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LogicBoot.Api.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly DbSet<T> _dbset;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            //_repositoryContext = repositoryContext;
            _dbset = repositoryContext.Set<T>();
        }

        public void Create(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _dbset.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public IQueryable<T> GetForeignkeyData(Expression<Func<T, bool>> expression, string entityName)
        {
            return _dbset.Include(entityName).Where(expression).AsNoTracking();
        }
    }
}
