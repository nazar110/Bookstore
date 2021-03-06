using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.ViewModels
{
    public class FilterViewModel
    {
        public string Filter { get; set; } = null;

        public FilterViewModel(string filterBy)
        {
            Filter = filterBy;
        }
    }
}
