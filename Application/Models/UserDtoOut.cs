namespace Application.Models
{
    public class UserDtoOut
    {
        public Guid? Id { get; set; }

        public string Login { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
