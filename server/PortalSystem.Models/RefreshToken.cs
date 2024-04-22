using System.ComponentModel.DataAnnotations;

namespace PortalSystem.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }
        public Guid UserId{ get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
