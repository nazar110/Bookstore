using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        // Add validation using Data Annotations
        public string Email { get; set; }
        // Add validation using Data Annotations
        public string Number { get; set; }
    }

}
