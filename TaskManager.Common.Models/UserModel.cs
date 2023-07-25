using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[]? Photo { get; set; }
        public UserStatus Status { get; set; }
        public UserModel(string fName, string lName, string email, string password, 
            UserStatus status, string phone) 
        {
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
            Status = status;
            Phone = phone;
            RegistrationDate = DateTime.Now;
        }
        public UserModel() { }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
