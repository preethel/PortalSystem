using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Models
{
    public class Class
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Timings are required")]
        public Timing? Timing { get; set; }
        [Required(ErrorMessage = "Maximum class size is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum class size must be greater than 0")]
        public int MaxClassSize { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public List<UserForClass> Users { get; set; } = [];
    }
    public class UserForClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
