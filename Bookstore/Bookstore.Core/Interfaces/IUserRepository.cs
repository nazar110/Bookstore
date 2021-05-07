using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Interfaces
{
    interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsersList();
        User GetUser(int id);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
        void Save();
    }
}
