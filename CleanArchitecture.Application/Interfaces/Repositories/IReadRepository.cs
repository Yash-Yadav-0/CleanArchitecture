﻿using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class, IEntityBase, new()
    {
        public Task<IList<T>> GetAllAsync
            (
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
            bool enableTracing = false
            );

        public Task<IList<T>> GetAllByPagningAsync
           (
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
          int currentPage = 1, int pageSize = 3,
           bool enableTracing = false
           );
        public Task<T> GetAsync
           (
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracing = false
           );
        Task<IQueryable<T>> Find(Expression<Func<T, bool>>? predicate, bool enableTracing = false);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
