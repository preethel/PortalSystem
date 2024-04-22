using PortalSystem.Models;
using PortalSystem.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Service.ClassService
{
    public class ServiceClass : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IUserRepository _userRepository;

        public ServiceClass(IClassRepository classRepository, IUserRepository userRepository)
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
        }

        public async Task<Class> CreateClassAsync(Class request)
        {
            if (await _classRepository.Add(request))
            {
                return request;
            }
            throw new Exception("Failed to save.");
        }

        public async Task<bool> DeleteClassAsync(Guid id)
        {
            if (await _classRepository.Delete(id))
            {
                return true;
            }
            throw new Exception("Failed to Delete.");
        }

        public async Task<Class> GetById(Guid id)
        {
            return await _classRepository.GetById(id);
        }

        public async Task<Class> EnrollByEmail(Guid classId, string email)
        {
            var enrollUser =await _userRepository.GetByEmail(email);
            var existingClass = await _classRepository.GetById(classId);
            if (existingClass != null && enrollUser !=null)
            {
                existingClass.Users.Add(new UserForClass
                {
                    Id = enrollUser.Id,
                    Name = enrollUser.Name,
                    Email = enrollUser.Email,

                });
                await _classRepository.Update(existingClass);
                return existingClass;
            }
            throw new Exception("Failed to enrollment.");
        }

        public async Task<List<Class>> GetClassListAsync(int page, int size)
        {
            return await _classRepository.GetAllAsync(page, size);
        }

        public async Task<Class> UpdateClassAsync(Class request)
        {
            if (await _classRepository.Update(request))
            {
                return request;
            }
            throw new Exception("Failed to Delete.");
        }
    }
}
