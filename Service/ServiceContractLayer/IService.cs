using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContractLayer
{
    /// <summary>
    /// Interfaz de contrato que expone las funcionalidades visibles en el API
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Método que permitira realizar una sumatoria
        /// </summary>
        /// <param name="additional">Objeto que encapsula los valores a sumar</param>
        /// <returns>Objeto que encapsula el resultado de la suma</returns>
        Additional Add(Additional additional);

        /// <summary>
        /// Método que permite realizar una resta
        /// </summary>
        /// <param name="substraction">Objeto que encapsula los valores a operar</param>
        /// <returns>Objeto que encapsula el resultado de la resta</returns>
        Substraction Sub(Substraction substraction);

        /// <summary>
        /// Método que permite realizar una multiplicación
        /// </summary>
        /// <param name="multiply">Objeto que encapsula los valores a multiplicar</param>
        /// <returns>Objeto que encapsulta el resultado de la multiplicación</returns>
        Multiply Mult(Multiply multiply);

        /// <summary>
        /// Método que permite realizar una división
        /// </summary>
        /// <param name="division">Objeto que encapsula los valores a dividir</param>
        /// <returns>Objeto que encapsulta el resultado de la división</returns>
        Division Div(Division division);

        /// <summary>
        /// Método que permite realizar la obtención de la raiz cuadrada de un número
        /// </summary>
        /// <param name="square">Objeto que encapsula el número al cual se le aplicará la operación</param>
        /// <returns>Objeto que encapsula el resultado de la operación</returns>
        Square Sqrt(Square square);

        /// <summary>
        /// Método que permite realizar la busqueda de las operaciones realizadas por un usuario
        /// </summary>
        /// <param name="journalQuery">Objeto que encapsulta el identificador del usuario por el cuál se desea hacer la busqueda</param>
        /// <returns>Objeto que encapsula la información de las operaciones realizadas</returns>
        JournalQuery JournalQuery(JournalQuery journalQuery);
    }
}
