﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TEST.Data;

namespace TEST.DAO;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return query.ToList();
    }

    public IEnumerable<T> GetEntities(Expression<Func<T, bool>>? filter, string? includeProperties = null, string? orderBy = null, bool isDescending = false)
    {
        IQueryable<T> query = _dbSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        // if (!string.IsNullOrEmpty(orderBy))
        // {
        //     query = query.OrderByProperty(orderBy, isDescending);
        // }

        return query.ToList();
    }

    public T? GetEntityById(object id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T obj)
    {
        _dbSet.Add(obj);
    }

    public void Update(T obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T? existing = _dbSet.Find(id);
        if (existing != null)
        {
            _dbSet.Remove(existing);
        }
    }
}