using LogicBoot.Api.Entities.Helper;
using LogicBoot.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBoot.Api.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        PagedList<Employee> GetEmployees(PaginationResourceParameters paginationResourceParameters);
    }
}
