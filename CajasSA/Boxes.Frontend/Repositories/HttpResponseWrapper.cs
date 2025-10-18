using System.Net;

namespace Boxes.Frontend.Repositories
{
    public class HttpResponseWrapper<T> //Generic class
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; } //only read property
        public bool Error { get; } //only read property
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }

            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta acción";
            }

            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permiso para ejecutar esta accion";
            }

            return "Ha ocurrido un error inesperado";
        }
    }
}
