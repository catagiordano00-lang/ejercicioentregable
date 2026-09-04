using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Repositores
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AplicationDbContext _context;

        public GenericRepository()
        {
            _context = new AplicationDbContext();

            _context.Database.EnsureCreated();
        }

        public void Agregar(T entity)
        {
            _context.Set<T>().Add(entity);

            _context.SaveChanges();
        }

        public List<T> ObtenerTodos()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> ObtenerTodosCon(string propiedad)
        {
            return _context.Set<T>()
                .Include(propiedad)
                .ToList();
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
