using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel(int id, string fullName, string email, bool active, DateTime createdAt)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Active = active;
            CreatedAt = createdAt;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
    }
}
