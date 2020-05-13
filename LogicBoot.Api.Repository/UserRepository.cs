using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities;
using LogicBoot.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBoot.Api.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
    }
}
