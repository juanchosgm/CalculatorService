using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Clase contenedora de las entidades a utilizar
    /// </summary>
    public static class CalculatorEntities
    {
        /// <summary>
        /// Constructor por defecto capaz de crea una nueva instancia para la propiedad <see cref="Operations"/> en caso de ser nula
        /// </summary>
        static CalculatorEntities()
        {
            if (Operations == null)
            {
                Operations = new List<Operation>();
            }
        }

        /// <summary>
        /// Propiedad de solo lectura para almacenar historico de las operaciones realizadas, estática para persistir la información
        /// </summary>
        public static IList<Operation> Operations { get; private set; }
    }
}
