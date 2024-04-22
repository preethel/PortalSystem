using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalSystem.Api.Dto;
using PortalSystem.Api.Shared;
using PortalSystem.Models;
using PortalSystem.Service.ClassService;
using System.Security.Claims;

namespace PortalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
      
        [HttpGet]
        public async Task<ActionResult> GetAllClasses(int page, int size)
        {
            var classes = await _classService.GetClassListAsync(page, size);
            return Ok(classes);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClassById(Guid id)
        {
            var result = await _classService.GetById(id);
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<ActionResult> CreateClass(ClassRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _classService.CreateClassAsync(new Class
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description,
                    GradeLevel = request.GradeLevelEnum,
                    Timing = request.Timing,
                    MaxClassSize = request.MaxClassSize,
                });
                return Ok(result);
            }
            else
            {
                return BadRequest(request);
            }
            throw new Exception("Invalid Request.");
            
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Update")]
        public async Task<ActionResult> UpdateClass(ClassRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _classService.UpdateClassAsync(new Class
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    GradeLevel = request.GradeLevelEnum,
                    Timing = request.Timing,
                    MaxClassSize = request.MaxClassSize,
                    Users = request.Users
                });
                return Ok(result);
            }
            else
            {
                return BadRequest(request);
            }
            throw new Exception("Invalid Request.");

        }
        [Authorize(Roles = "admin")]
        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteClass(ClassDeleteRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _classService.DeleteClassAsync(request.Id);
                return Ok(result);
            }
            
            throw new Exception("Invalid Request.");
        }

        [Authorize(Roles = "user")]
        [HttpPost("enroll")]
        public async Task<ActionResult> EnrollClass(EnrollRequest request)
        {
            // Retrieve user email from JWT token
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


            if (ModelState.IsValid)
            {
                var result = await _classService.EnrollByEmail(request.ClassId, userEmail);
                return Ok(new ServiceResponse<int>{
                   Data = 1,
                   Message = "Enroll succeed",
                   Success = true
                });
            }
            
            throw new Exception("Invalid Request.");

        }
    }
}
