using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Interfaz de repositorio para interación con la capa de datos
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity: class
    {
        /// <summary>
        /// Método que permite crear una nueva entidad en un almacenador de datos
        /// </summary>
        /// <param name="entity">Nueva entidad que se va a crear</param>
        void Create(TEntity entity);

        /// <summary>
        /// Método que permite filtrar y extraer las entidades por un criterio de busqueda del almacenador de datos
        /// </summary>
        /// <param name="criteria">Criterio de busqueda por el cuál se desea filtrar</param>
        /// <returns>Retorna todas las entidades que cumplan con el filtro de busqueda</returns>
        IEnumerable<TEntity> Filter(Func<TEntity, bool> criteria);
    }
}
