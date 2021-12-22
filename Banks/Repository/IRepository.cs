using System;
using System.Collections.Generic;

namespace Banks.Repository
{
    public interface IRepository<T>
    {
        void Save(T objectForSaving);

        IEnumerable<T> GetAll();

        T Get(Guid id);
    }
}