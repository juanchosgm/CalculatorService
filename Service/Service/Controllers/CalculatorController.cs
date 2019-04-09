using BusinessLogicLayer;
using Entities;
using Newtonsoft.Json;
using ServiceContractLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Service.Controllers
{
    public class CalculatorController : ApiController, IService
    {
        public const string headerName = "X-Evi-Tracking-Id";

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

        [HttpPost]
        [Route("calculator/add")]
        public Additional Add(Additional additional)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Addition(additional.Addends);
                return new Additional
                {
                    Sum = result
                };
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

        public (int Quotient, int Remainder) Div(int? dividend, int? divisor)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Division(dividend, divisor);
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

        public IEnumerable<Operation> JournalQuery()
        {
            var businessLayer = new Operations(HeaderValue);
            var result = businessLayer.JournalQuery();
            return result;
        }

        public int Mult(int?[] factors)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Multiply(factors);
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

        public double Sqrt(int? number)
        {
            var businessLayer = new Operations(HeaderValue);
            try
            {
                var result = businessLayer.Square(number);
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

        public int Sub(int? minuend, int? subtrahend)
        {
            throw new NotImplementedException();
        }

        protected HttpResponseException HandledError(HttpStatusCode httpStatusCode, string message)
        {
            var bodyContent = JsonConvert.SerializeObject(new
            {
                ErrorCode = "InternalError",
                ErrorStatus = httpStatusCode,
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
