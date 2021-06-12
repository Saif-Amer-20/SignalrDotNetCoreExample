using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.Events
{
    public class ClientAddedEventArgs : EventArgs
    {
        public ClientAddedEventArgs(int id, string firstName, string lastName, string email, string category)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Category = category;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
    }
}
