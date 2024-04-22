using PortalSystem.Models;
using PortalSystem.Repository.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository.Abstractions
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByIdAsync(string id);
        Task<bool> DeleteRefreshTokenByToken(Guid token);
    }
}
