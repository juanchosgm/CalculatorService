using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Clase que fabrica los repositorios
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// Permite fabricar un nuevo repositorio de la clase <see cref="StaticListRepository"/>
        /// </summary>
        /// <returns>Retorna una nueva instancia del repositorio</returns>
        public static IRepository<Operation> CreateStaticOperationRepository()
        {
            return new StaticListRepository();
        }
    }
}
