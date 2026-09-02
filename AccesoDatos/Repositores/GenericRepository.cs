using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Repositores
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly AplicationDbContext _context;
        public GenericRepository(DbContextOptions<AplicationDbContext> options)
        {
            _context = new AplicationDbContext(options);
            _context.Database.EnsureCreated();
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public void Modificar(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Eliminar(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
