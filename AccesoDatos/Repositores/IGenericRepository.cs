using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositores
{
    public interface IGenericRepository<T> where T : class
    {
        void Agregar(T entity);

        List<T> ObtenerTodos();

        List<T> ObtenerTodosCon(string propiedad);

        void Modificar(T entity);

        void Eliminar(T entity);
    }
}
