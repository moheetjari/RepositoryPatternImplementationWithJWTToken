using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBoot.Api.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void save();
    }
}
