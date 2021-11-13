using System;
using System.Collections.Generic;

namespace Isu.Repository
{
    public interface IRepository<T>
    {
        void Save(T thingWeSave);

        T Get(Guid id);

        List<T> GetAll();
    }
}