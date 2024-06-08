﻿using Microsoft.EntityFrameworkCore;
using Milliygram.Data.DbContexts;
using Milliygram.Domain.Commons;
using System.Linq.Expressions;

namespace Milliygram.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this.context = context;
        set = context.Set<T>();
    }

    public async Task<T> InsertAsync(T entity)
    {
        return (await set.AddAsync(entity)).Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async Task<T> DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async Task<T> DropAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracked = true)
    {
        var query = set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> SelectAsEnumerableAsync(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.ToListAsync();
    }

    public IQueryable<T> SelectAsQueryable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();


        return query;
    }
}