using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskModule_Web.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Collection();
        void Commit();
        T Find(int Id);
        void Insert(T t);
        void Delete(int Id);
        void Edit(T t);

    }
}