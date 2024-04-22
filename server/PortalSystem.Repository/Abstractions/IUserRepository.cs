using PortalSystem.Models;
using PortalSystem.Repository.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository.Abstractions
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid Id);
    }
}
