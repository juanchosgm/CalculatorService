using BusinessLogicLayer;
using Entities;
using Newtonsoft.Json;
using ServiceContractLayer;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Services.Controllers
{
    /// <summary>
    /// Servicio de tipo HTTP/REST para exponer los métodos de comunicación con la lógica de negocio
    /// </summary>
    public class CalculatorController : ApiController, IService
    {
        /// <summary>
        /// Nombre del encabezado personalizado
        /// </summary>
        public const string headerName = "X-Evi-Tracking-Id";

        /// <summary>
        /// Propiedad que extrae el valor que se encuentra en el encabezado personalizado. Si no se encuentra la llave, habrá una excepción
        /// </summary>
        public int HeaderValue
        {
            get
            {
                if (!Request.Headers.Contains(headerName))
                {
                    throw HandledError(HttpStatusCode.BadRequest, "The header X‐Evi‐Tracking‐Id has not been sent");
                }
                return int.Parse(Request.Headers.GetValues(headerName).First());
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para la suma
        /// </summary>
        /// <param name="additional">Objeto que encapsula los números a sumar</param>
        /// <returns>Retorna un objeto que encapsula el resultado</returns>
        [HttpPost]
        [Route("calculator/add")]
        public Additional Add(Additional additional)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Addition(additional.Addends);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para la división
        /// </summary>
        /// <param name="division">Objeto que encapsula los números que se van a dividir</param>
        /// <returns>Retorna un objeto que encapsula el resultado</returns>
        [HttpPost]
        [Route("calculator/div")]
        public Division Div(Division division)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Division(division.Dividend, division.Divisor);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch (DivideByZeroException dbze)
            {
                throw HandledError(HttpStatusCode.BadRequest, dbze.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para consultar todas las operaciones realizadas por un usuario
        /// </summary>
        /// <param name="journalQuery">Objeto que encapsula el identificador del usuario</param>
        /// <returns>Retorna un objeto que encapsula todas las operaciones realizadas por un usuario</returns>
        [HttpPost]
        [Route("journal/query")]
        public JournalQuery JournalQuery(JournalQuery journalQuery)
        {
            var businessLayer = new Operations();
            try
            {
                var result = businessLayer.JournalQuery(journalQuery.Id);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para la multiplicación
        /// </summary>
        /// <param name="multiply">Objeto que encapsula la información de los números a multiplicar</param>
        /// <returns>Retorna un objeto con el resultado de la multiplicación</returns>
        [HttpPost]
        [Route("calculator/mult")]
        public Multiply Mult(Multiply multiply)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Multiply(multiply.Factors);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para obtener la raiz cuadrada de un número
        /// </summary>
        /// <param name="square">Objeto que encapsula la información del número a operar</param>
        /// <returns>Retorna un objeto que encapsula el resultado de la operación</returns>
        [HttpPost]
        [Route("sqrt")]
        public Square Sqrt(Square square)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Square(square.Number);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método que invoca la capa de negocio y expone su lógica como un API para restar
        /// </summary>
        /// <param name="substraction">Objeto que encapsula la información de los números a restar</param>
        /// <returns>Retorna un objeto que encapsula el resultado de la resta</returns>
        [HttpPost]
        [Route("calculator/sub")]
        public Substraction Sub(Substraction substraction)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Subtraction(substraction.Minuend, substraction.Subtrahend);
                return result;
            }
            catch (InvalidOperationException ioex)
            {
                throw HandledError(HttpStatusCode.BadRequest, ioex.Message);
            }
            catch
            {
                throw HandledError(HttpStatusCode.InternalServerError, "Has occurred a error, please try again or contact your administrator");
            }
        }

        /// <summary>
        /// Método para el manejo de excepciones hacia el usuario
        /// </summary>
        /// <param name="httpStatusCode">Codigo HTTP del error causado en cada operación</param>
        /// <param name="message">Mensaje de guía para el usuario</param>
        /// <returns>Objeto que encapsula un mensaje HTTP para la respuesta al usuario</returns>
        protected HttpResponseException HandledError(HttpStatusCode httpStatusCode, string message)
        {
            var bodyContent = JsonConvert.SerializeObject(new Error
            {
                ErrorCode = "InternalError",
                ErrorStatus = (int)httpStatusCode,
                ErrorMessage = message
            });
            return new HttpResponseException(new HttpResponseMessage
            {
                Content = new StringContent(bodyContent, Encoding.UTF8, "application/json"),
                StatusCode = httpStatusCode
            });
        }
    }
}