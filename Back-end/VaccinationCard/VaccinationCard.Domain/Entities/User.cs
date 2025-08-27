namespace VaccinationCard.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; private set; }

        public string Password { get; private set; }
        
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}
