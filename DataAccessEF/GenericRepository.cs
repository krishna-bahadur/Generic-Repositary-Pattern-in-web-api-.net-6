using Azure.Core;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEF
{
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Id is null or invalid");
            }
            T existing = _entities.Find(id);
            if (existing != null)
                _entities.Remove(existing);
            else
                throw new NullReferenceException("Can't find entity by Id");
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
           
        }

        public async Task<T> GetById(int id)
        {
            if (id <= 0)
            {
                throw new CustomNullException("Id is null or invalid");
            }
            var entity = await _entities.FindAsync(id);
            if (entity != null)
                return entity;
            else
                throw new CustomException("Item Not Found");
        }

        public void Add(T model)
        {
            try
            {
                _entities.Add(model);
            }
            catch
            {
                throw new NullReferenceException("No item found in database");
            }
        }

        public async Task<int> Save()
        {
           return await _context.SaveChangesAsync();
        }


        public bool Exists()
        {
            return _entities.Any();
        }

        public void Edit(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
