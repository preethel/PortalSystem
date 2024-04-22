using PortalSystem.Models;

namespace PortalSystem.Service.ClassService
{
    public interface IClassService
    {
        Task<List<Class>> GetClassListAsync(int page, int size);
        Task<Class> CreateClassAsync(Class request);
        Task<Class> UpdateClassAsync(Class request);
        Task<bool> DeleteClassAsync(Guid Id);
        Task<Class> GetById(Guid id);
        Task<Class> EnrollByEmail(Guid classId, string email);
    }
}
