using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Interfaces
{
    public interface IDataLyfr<T> where T : class
    {
        string Insert(T item);
        string Alter(T item);
        string Delete(T item);
    }
}
