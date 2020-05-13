using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities;
using LogicBoot.Api.Entities.Helper;
using LogicBoot.Api.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LogicBoot.Api.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly DbSet<Employee> _dbSet;

        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _dbSet = repositoryContext.Set<Employee>();
        }

        public PagedList<Employee> GetEmployees(PaginationResourceParameters paginationResourceParameters)
        {
            IQueryable<Employee> query = _dbSet;
            return PagedList<Employee>.Create(query, paginationResourceParameters.PageNumber, paginationResourceParameters.PageSize);
        }
    }
}
