using System.ComponentModel.DataAnnotations;

namespace Syfora_Test.Models
{
    public class UserDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
