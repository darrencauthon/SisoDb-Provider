﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SisoDb.Querying;

namespace SisoDb
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Insert<T>(T item) where T : class;

        void InsertMany<T>(IEnumerable<T> items) where T : class;

        void Update<T>(T item) where T : class;

        bool DeleteById<T>(Guid id) where T : class;

        bool DeleteById<T>(int id) where T : class;

        int Count<T>() where T : class;

        T GetById<T>(Guid id) where T : class;

        T GetById<T>(int id) where T : class;

        string GetByIdAsJson<T>(Guid id) where T : class;

        string GetByIdAsJson<T>(int id) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;

        IEnumerable<string> GetAllAsJson<T>() where T : class;

        IEnumerable<T> NamedQuery<T>(INamedQuery query) where T : class;

        IEnumerable<string> NamedQueryAsJson<T>(INamedQuery query) where T : class;

        IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class;

        IEnumerable<string> QueryAsJson<T>(Expression<Func<T, bool>> expression) where T : class;
    }
}