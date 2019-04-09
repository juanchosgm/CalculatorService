using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Clase que implementa la interfaz de repositorio para ejecutar todas las operaciones
    /// </summary>
    public class StaticListRepository : IRepository<Operation>
    {
        /// <summary>
        /// Agrega una nueva operación a la colección de operaciones que existe en el contexto de entidades
        /// </summary>
        /// <param name="entity">Nueva operación</param>
        public void Create(Operation entity)
        {
            CalculatorEntities.Operations.Add(entity);
        }

        /// <summary>
        /// Filtra de la colección de operaciones, aquellas que cumplan con el criterio de busqueda
        /// </summary>
        /// <param name="criteria">Criterio de busqueda por el cuál se filtraran las operaciones</param>
        /// <returns>Retorna aquellas operaciones que cumplan con la condición específicada</returns>
        public IEnumerable<Operation> Filter(Func<Operation, bool> criteria)
        {
            var operations = CalculatorEntities.Operations.Where(criteria);
            return operations;
        }
    }
}
