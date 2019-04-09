using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Clase que implementa la lógica de negocio de las operaciones
    /// </summary>
    public class Operations
    {
        /// <summary>
        /// Campo que almacena el identificador del usuario realizando la operación
        /// </summary>
        private readonly int xEviTrackingId;

        /// <summary>
        /// Constructor por defecto, sin lógica
        /// </summary>
        public Operations()
        {
        }

        /// <summary>
        /// Constructor que recibe el identificador de usuario y lo asigna al campo <see cref="xEviTrackingId"/>
        /// </summary>
        /// <param name="xEviTrackingId">Identificador del usuario</param>
        public Operations(int xEviTrackingId)
        {
            this.xEviTrackingId = xEviTrackingId;
        }

        /// <summary>
        /// Método que registra todas las operaciones matemáticas en la capa de acceso a datos
        /// </summary>
        /// <param name="operation">Operación que se va a registrar</param>
        private void RegisterOperation(Operation operation)
        {
            var repository = RepositoryFactory.CreateStaticOperationRepository();
            repository.Create(operation);
        }

        /// <summary>
        /// Método que permite realizar la suma de los números enteros que vienen en el arreglo, si hay algún número nulo, habrá una excepción
        /// </summary>
        /// <param name="addends">Arreglo de números a sumar</param>
        /// <returns>Retorna un objeto con el resultado de la suma</returns>
        public Additional Addition(params int?[] addends)
        {
            var builder = new StringBuilder();
            foreach (var addend in addends)
            {
                if (!addend.HasValue)
                {
                    throw new InvalidOperationException($"Some values have incorrect format");
                }
                builder.Append($"{addend} + ");
            }
            var calculation = builder.ToString();
            var result = addends.Sum().Value;
            var operation = new Operation
            {
                XEviTrackingId = xEviTrackingId,
                Date = DateTime.Now,
                OperationName = "Sum",
                Calculation = $"{calculation.Substring(byte.MinValue, calculation.Length - 2)}= {result}"
            };
            RegisterOperation(operation);
            return new Additional
            {
                Sum = result
            };
        }

        /// <summary>
        /// Método que realiza una resta entre dos números, si alguno de los parámetros es nulo, habrá una excepción
        /// </summary>
        /// <param name="minuend">Número que se desea restar</param>
        /// <param name="subtrahend">Valor por el cuál se realiza la resta</param>
        /// <returns>Retorna un objeto con el resultado de la resta</returns>
        public Substraction Subtraction(int? minuend, int? subtrahend)
        {
            if (!minuend.HasValue)
            {
                throw new InvalidOperationException("Incorrect value input in minuend");
            }
            if (!subtrahend.HasValue)
            {
                throw new InvalidOperationException("Incorrect value input in subtrahend");
            }
            var result = (minuend + subtrahend).Value;
            var operation = new Operation
            {
                XEviTrackingId = xEviTrackingId,
                Date = DateTime.Now,
                OperationName = "Sub",
                Calculation = $"{minuend} + {subtrahend} = {result}"
            };
            RegisterOperation(operation);
            return new Substraction
            {
                Difference = result
            };
        }

        /// <summary>
        /// Método que realiza una multiplicación de números enteros contenidos en un arreglo, si hay algún número nulo, habrá una excepción
        /// </summary>
        /// <param name="factors">Arreglo de números a multiplicar</param>
        /// <returns>Retorna un objeto con el resultado de la multiplicación</returns>
        public Multiply Multiply(params int?[] factors)
        {
            var builder = new StringBuilder();
            var result = 1;
            foreach (var factor in factors)
            {
                if (!factor.HasValue)
                {
                    throw new InvalidOperationException($"Some values have incorrect format");
                }
                result *= factor.Value;
                builder.Append($"{factor} * ");
            }
            var calculation = builder.ToString();
            var operation = new Operation
            {
                XEviTrackingId = xEviTrackingId,
                Date = DateTime.Now,
                OperationName = "Mul",
                Calculation = $"{calculation.Substring(byte.MinValue, calculation.Length - 2)}= {result}"
            };
            RegisterOperation(operation);
            return new Multiply
            {
                Product = result
            };
        }

        /// <summary>
        /// Método que permite realizar una división entre dos números, si el divisor es 0 ó alguno de los parámetros es nulo, habrá una excepción
        /// </summary>
        /// <param name="dividend">Valor que se desea dividir</param>
        /// <param name="divisor">Valor por el cual se va a dividir</param>
        /// <returns>Retorna un objeto con el resultado de la división</returns>
        public Division Division(int? dividend, int? divisor)
        {
            if (!dividend.HasValue)
            {
                throw new InvalidOperationException("Incorrect value input in dividend");
            }
            if (!divisor.HasValue)
            {
                throw new InvalidOperationException("Incorrect value input in divisor");
            }
            else if (divisor.Value == 0)
            {
                throw new DivideByZeroException("The divisor cannot zero");
            }
            var quotient = Math.DivRem(dividend.Value, divisor.Value, out var remainder);
            var operation = new Operation
            {
                XEviTrackingId = xEviTrackingId,
                Date = DateTime.Now,
                OperationName = "Div",
                Calculation = $"{dividend} / {divisor} = {dividend / divisor}"
            };
            RegisterOperation(operation);
            return new Division
            {
                Quotient = quotient,
                Remainder = remainder
            };
        }

        /// <summary>
        /// Método que permite obtener la raiz cuadrada de un número, si el parámetro de entrada es nulo, habrá excepción
        /// </summary>
        /// <param name="number">Número al cual se desea extraer la raiz cuadrada</param>
        /// <returns>Retorna un objeto con el resultado de la operación matemática</returns>
        public Square Square(int? number)
        {
            if (!number.HasValue)
            {
                throw new InvalidOperationException("Incorrect value input in number");
            }
            else if (number < 0)
            {
                throw new InvalidOperationException("Cannot get the square for negative numbers");
            }
            var result = Math.Sqrt((double)number);
            var operation = new Operation
            {
                XEviTrackingId = xEviTrackingId,
                Date = DateTime.Now,
                OperationName = "Squ",
                Calculation = $"sqrt({number}) = {result}"
            };
            RegisterOperation(operation);
            return new Square
            {
                SquareResult = result
            };
        }

        /// <summary>
        /// Método que consulta todas las operaciones que ha hecho un usuario, si no se envia identificador de usuario, habrá una excepción
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Retorna un objeto con las operaciones realizadas por el usuario filtrado</returns>
        public JournalQuery JournalQuery(int? id)
        {
            if (!id.HasValue)
            {
                throw new InvalidOperationException("The id value cannot be empty");
            }
            var repository = RepositoryFactory.CreateStaticOperationRepository();
            var result = repository.Filter(o => o.XEviTrackingId == id);
            return new JournalQuery
            {
                Operations = result
            };
        }
    }
}
