using System.ComponentModel.DataAnnotations;

namespace Syfora_Test.Models
{
    public class UserDtoIn
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
