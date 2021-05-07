using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookstore.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        // Add validation using Data Annotations
        [RegularExpression(@"^[a-zA-Z0-9@gmail.com|a-zA-Z0-9@ukr.net|a-zA-Z0-9@mail.ru]*$")]
        public string Email { get; set; }
        // Add validation using Data Annotations
        [RegularExpression(@"[+]?[380|48|375|1|44|370|371|372][0-9]{9}")]
        public string Number { get; set; }

        public List<Order> Orders { get; set; }
    }

}
