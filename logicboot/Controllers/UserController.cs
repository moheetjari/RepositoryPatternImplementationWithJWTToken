using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LogicBoot.Api.Contracts;
using LogicBoot.Api.Entities.Models;
using LogicBoot.Api.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogicBoot.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User

        private IRepositoryWrapper _repoWrapper;

        public UserController(IRepositoryWrapper repositoryWrapper)
        {
            _repoWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IQueryable<User> Get()
        {
            var getUser = _repoWrapper.User.FindAll();
            return getUser;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post(User user)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                String _password = Common.GetMd5Hash(md5Hash, user.Password);
                String _doubleEncryption = Common.GetMd5Hash(md5Hash, _password);
                user.Password = _doubleEncryption;
                user.RoleId = 2;
                _repoWrapper.User.Create(user);
                _repoWrapper.save();
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
