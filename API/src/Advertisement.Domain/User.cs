using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public bool Active { get; set; } = true;
    }
}