using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBoot.Api.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IEmployeeRepository _employee;
        private IUserRepository _user;
        private IRoleRepository _role;

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext);
                }
                return _employee;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }
                return _role;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void save()
        {
            _repoContext.SaveChanges();
        }
    }
}
