using System;

namespace DevFreela.Application.InputModels
{
    public class NewUserInputModel
    {
        public NewUserInputModel(string fullName, string email, DateTime birthDate, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            Password = password;
            Role = role;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
    }
}
