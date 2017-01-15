﻿namespace CampBg.Data.Contracts
{
    using System.Linq;

    public interface IRepository<T>
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}
