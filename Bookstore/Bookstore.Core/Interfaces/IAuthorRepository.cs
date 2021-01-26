using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core.Interfaces
{
    public interface IAuthorRepository : IDisposable
    {
        IEnumerable<Author> GetAuthorsList();
        Author GetAuthor(int id);
        void Create(Author item);
        void Update(Author item);
        void Delete(int id);
        void Save();
    }
}
