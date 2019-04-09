using Entities;
using Newtonsoft.Json;
using ServiceContractLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProxyService
{
    /// <summary>
    /// Clase proxy que provee los métodos de conexión disponibles en el servicio
    /// </summary>
    public class Proxy : IService
    {
        /// <summary>
        /// Dirección del WEB API donde se va a conectar la aplicación
        /// </summary>
        private readonly string baseAddress = "http://localhost:54733/";
        /// <summary>
        /// Identificador con el cuál se va a hacer la operación
        /// </summary>
        private readonly int id;

        /// <summary>
        /// Constructor que asigna el valor al identificador
        /// </summary>
        /// <param name="id">Identificador de la transacción</param>
        public Proxy(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Método que realiza invocaciones a aquellos WEB API de verbo POST, para administrarlo en un sólo punto
        /// </summary>
        /// <typeparam name="T">Tipo de parametro que se va a devolver</typeparam>
        /// <typeparam name="TPostData">Tipo de parametro que se va a serializar</typeparam>
        /// <param name="requestUri">Nombre de la ruta del WEB API</param>
        /// <param name="data">Objeto que se va a serializar</param>
        /// <returns>Retorna una tarea del resultado de cada operación</returns>
        public async Task<T> SendPost<T, TPostData>(string requestUri, TPostData data)
        {
            var result = default(T);
            using (var client = new HttpClient())
            {
                requestUri = $"{baseAddress}{requestUri}";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", id.ToString());
                var jsonData = JsonConvert.SerializeObject(data);
                var request = await client.PostAsync(requestUri, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                var response = await request.Content.ReadAsStringAsync();
                if (request.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(response);
                }
                else
                {
                    var errorMessage = JsonConvert.DeserializeObject<Error>(response);
                    throw new InvalidOperationException(errorMessage.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// Método que invoca al servicio calculator/add - Ideal para aplicaciones móviles
        /// </summary>
        /// <param name="additional">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<Additional> AddAsync(Additional additional)
        {
            return await SendPost<Additional, Additional>("calculator/add", additional);
        }

        /// <summary>
        /// Método que invoca <see cref="AddAsync(Additional)"/> de forma sincrona
        /// </summary>
        /// <param name="additional">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public Additional Add(Additional additional)
        {
            var result = default(Additional);
            Task.Run(async () => result = await AddAsync(additional)).Wait();
            return result;
        }

        /// <summary>
        /// Método que invoca al servicio calculator/div - Ideal para aplicaciones móviles
        /// </summary>
        /// <param name="division">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<Division> DivAsync(Division division)
        {
            return await SendPost<Division, Division>("calculator/div", division);
        }

        /// <summary>
        /// Método que invoca <see cref="DivAsync(Division)"/> de forma sincrona
        /// </summary>
        /// <param name="division">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public Division Div(Division division)
        {
            var result = default(Division);
            Task.Run(async () => result = await DivAsync(division)).Wait();
            return result;
        }

        /// <summary>
        /// Método que invoca el serivicio journal/query - Ideal para aplicaciones móviles
        /// </summary>
        /// <param name="journalQuery">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<JournalQuery> JournalQueryAsync(JournalQuery journalQuery)
        {
            return await SendPost<JournalQuery, JournalQuery>("journal/query", journalQuery);
        }

        /// <summary>
        /// Método que invoca <see cref="JournalQueryAsync(Entities.JournalQuery)"/> de forma sincrona
        /// </summary>
        /// <param name="journalQuery">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public JournalQuery JournalQuery(JournalQuery journalQuery)
        {
            var result = default(JournalQuery);
            Task.Run(async () => result = await JournalQueryAsync(journalQuery)).Wait();
            return result;
        }

        /// <summary>
        /// Método que invoca el servicio calculator/mult - Ideal para aplicaciones móviles
        /// </summary>
        /// <param name="multiply">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<Multiply> MultAsync(Multiply multiply)
        {
            return await SendPost<Multiply, Multiply>("calculator/mult", multiply);
        }

        /// <summary>
        /// Método que invoca <see cref="MultAsync(Multiply)"/> de forma sincrona
        /// </summary>
        /// <param name="multiply">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public Multiply Mult(Multiply multiply)
        {
            var result = default(Multiply);
            Task.Run(async () => result = await MultAsync(multiply)).Wait();
            return result;
        }

        /// <summary>
        /// Método que invoca el servicio sqrt - Ideal para aplicaciones móviles
        /// </summary>
        /// <param name="square">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<Square> SqrtAsync(Square square)
        {
            return await SendPost<Square, Square>("sqrt", square);
        }

        /// <summary>
        /// Método que invoca <see cref="SqrtAsync(Square)"/> de forma sincrona
        /// </summary>
        /// <param name="square">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public Square Sqrt(Square square)
        {
            var result = default(Square);
            Task.Run(async () => result = await SqrtAsync(square)).Wait();
            return result;
        }

        /// <summary>
        /// Método que invoca el servicio calculator/sub - Ideal para móviles
        /// </summary>
        /// <param name="substraction">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna una tarea con el resultado de la operación</returns>
        public async Task<Substraction> SubAsync(Substraction substraction)
        {
            return await SendPost<Substraction, Substraction>("calculator/sub", substraction);
        }

        /// <summary>
        /// Método que invoca <see cref="SubAsync(Substraction)"/> de forma sincrona
        /// </summary>
        /// <param name="substraction">Objeto que contiene parametros de entrada</param>
        /// <returns>Retorna el resultado de la operación</returns>
        public Substraction Sub(Substraction substraction)
        {
            var result = default(Substraction);
            Task.Run(async () => result = await SubAsync(substraction)).Wait();
            return result;
        }
    }
}
