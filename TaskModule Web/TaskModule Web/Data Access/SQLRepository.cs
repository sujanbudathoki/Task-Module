using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskModule_Web.Repositories;

namespace TaskModule_Web.Data_Access
{
    public class SQLRepository<T> : IRepository<T> where T :class
    {
        internal DataContext context;
        internal DbSet<T> dbset;
        public SQLRepository(DataContext context)
            {
            this.dbset = context.Set<T>();
            this.context = context;
            }
        

        public IQueryable<T> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbset.Attach(t);
            dbset.Remove(t);
        }

        public void Edit(T t)
        {
            dbset.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public T Find(int Id)
        {
            var t = dbset.Find(Id);
            return t;
        }

        public void Insert(T t)
        {
            dbset.Add(t);
        }
    }
}