using System.Text;
using System.Text.Json;

namespace Boxes.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // to ignore case sensitivity when deserializing json
        };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //deserialize the response to the type T
        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url) // url controller endpoint to wait for the response
        {
            var httpResponse = await _httpClient.GetAsync(url); // wait for the response from the api
            if (httpResponse.IsSuccessStatusCode) // if the response is successful
            {
                var response = await UnserializeAnswerAsync<T>(httpResponse); // deserialize the response to the type T
                return new HttpResponseWrapper<T>(response, false, httpResponse); // return the response wrapped in HttpResponseWrapper
            }

            return new HttpResponseWrapper<T>(default, true, httpResponse); // return default value of T (null for reference types) and error true
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model); // serialize the model to json
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json"); // create the content to send
            var httpResponse = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }

        //serialize the response to the type TActionResponse
        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model); // serialize the model to json
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json"); // create the content to send
            var httpResponse = await _httpClient.PostAsync(url, messageContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswerAsync<TActionResponse>(httpResponse);
                return new HttpResponseWrapper<TActionResponse>(response, false, httpResponse);
            }

            return new HttpResponseWrapper<TActionResponse>(default, true, httpResponse);
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url)
        {
            var httpResponse = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }

        public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model); // serialize the model to json
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json"); // create the content to send
            var httpResponse = await _httpClient.PutAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model); // serialize the model to json
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json"); // create the content to send
            var httpResponse = await _httpClient.PutAsync(url, messageContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswerAsync<TActionResponse>(httpResponse);
                return new HttpResponseWrapper<TActionResponse>(response, false, httpResponse);
            }

            return new HttpResponseWrapper<TActionResponse>(default, true, httpResponse);
        }

        private async Task<T> UnserializeAnswerAsync<T>(HttpResponseMessage httpResponse)
        {
            var response = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonSerializerOptions)!; // may be null
        }
    }
}
