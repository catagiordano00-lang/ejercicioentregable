using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositores
{
    internal interface IGenericRepository <T> where T : class

    {
        void Add(T entity);
        List<T> GetAll();
        
    }
}
