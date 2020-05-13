using System.Collections.Generic;
using System.Linq;
using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities.Helper;
using LogicBoot.Api.Entities.Models;
using LogicBoot.Api.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LogicBoot.Api.Entities.Helper.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace logicboot.Controllers
{
    /// <summary>
    /// Employee Table
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<controller>
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// Employee Controller Constructor
        /// </summary>
        /// <param name="repositoryWrapper"></param>
        /// <param name="urlHelper"></param>
        public EmployeeController(IRepositoryWrapper repositoryWrapper, IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Get all Employee Data
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpGet(Name = "GetAllEmployee")]
        public IActionResult GetAllEmployee([FromQuery]PaginationResourceParameters paginationResourceParameters)
        {
            var employees = _repositoryWrapper.Employee.GetEmployees(paginationResourceParameters);

            var previousPageLink = employees.HasPrevious ?
                CreateResourceUri(paginationResourceParameters, ResourceUriType.Previous) : null;

            var nextPageLink = employees.HasNext ?
                CreateResourceUri(paginationResourceParameters, ResourceUriType.Next) : null;

            var paginationMetaData = new
            {
                totalCount = employees.TotalCount,
                pageSize = employees.PageSize,
                currentPage = employees.CurrentPage,
                totalPage = employees.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            ResponseModel<Employee> responseModel = new ResponseModel<Employee>()
            {
                Items = employees.ToList(),
                HasPreviousPage = employees.HasPrevious,
                PreviousPageLink = previousPageLink,
                HasNextPage = employees.HasNext,
                NextPageLink = nextPageLink,
                TotalCount = employees.TotalCount,
                PageSize = employees.PageSize,
                CurrentPage = employees.CurrentPage,
                TotalPage = employees.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetaData));

            return Ok(responseModel);
        }

        private string CreateResourceUri(PaginationResourceParameters paginationResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.Previous:
                    var x = _urlHelper.Link("GetAllEmployee", new
                    {
                        pageNumber = paginationResourceParameters.PageNumber - 1,
                        pageSize = paginationResourceParameters.PageSize
                    });
                    return x;

                case ResourceUriType.Next:
                    return _urlHelper.Link("GetAllEmployee", new
                    {
                        pageNumber = paginationResourceParameters.PageNumber + 1,
                        pageSize = paginationResourceParameters.PageSize
                    });

                default:
                    return _urlHelper.Link("GetAllEmployee", new
                    {
                        pageNumber = paginationResourceParameters.PageNumber,
                        pageSize = paginationResourceParameters.PageSize
                    });
            }
        }

        /// <summary>
        /// Get Employee By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IQueryable<Employee> Get(int id)
        {
            var employee = _repositoryWrapper.Employee.FindByCondition(x => x.empId == id);
            return employee;
        }

        /// <summary>
        /// Add Employee Data
        /// </summary>
        /// <param name="emp"></param>
        // POST api/<controller>
        [HttpPost]
        public void Post(Employee emp)
        {
            _repositoryWrapper.Employee.Create(emp);
            _repositoryWrapper.save();
        }

        /// <summary>
        /// Update Employee Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emp"></param>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, Employee emp)
        {
            IQueryable<Employee> empData = _repositoryWrapper.Employee.FindByCondition(x => x.empId == id);
            Employee empObj = empData.FirstOrDefault();
            if (empObj != null)
            {
                empObj.empName = emp.empName;
                empObj.empAddress = emp.empAddress;
                empObj.empMobileno = emp.empMobileno;
                _repositoryWrapper.Employee.Update(empObj);
                _repositoryWrapper.save();
            }
        }

        /// <summary>
        /// Delete Employee Data
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IQueryable<Employee> empData = _repositoryWrapper.Employee.FindByCondition(x => x.empId == id);
            Employee emp = empData.FirstOrDefault();
            if (emp != null)
            {
                _repositoryWrapper.Employee.Delete(emp);
                _repositoryWrapper.save();
            }
        }
    }
}
