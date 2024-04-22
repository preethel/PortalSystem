using PortalSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace PortalSystem.Api.Dto
{
    public class ClassRequest
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Title are required")]
        public string Title { get; set; }
        public string Description { get; set; }
        
        public Timing Timing { get; set; }

        [Required(ErrorMessage = "Maximum class size is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum class size must be greater than 0")]
        public int MaxClassSize { get; set; }

        [Required(ErrorMessage = "Grade level is required")]
        public GradeLevel GradeLevelEnum { get; set; }
        public List<UserForClass>? Users { get; set; }

    }

}
