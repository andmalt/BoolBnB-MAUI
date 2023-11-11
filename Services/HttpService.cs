using BoolBnB_MAUI.Services.Interface;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace BoolBnB_MAUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://8100-87-7-229-26.ngrok-free.app";
        public HttpService() {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json")
               );
        }
        public async Task<HttpResponseMessage> Get(string url, string token = null)
        {
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {
                return await _httpClient.GetAsync(uri);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR GET REQUEST: {0}",ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> Patch(string url, object data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await _httpClient.PatchAsync(uri, content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR PATCH REQUEST: {0}", ex.Message);
                throw;
            }
}

        public async Task<HttpResponseMessage> Post(string url, object data = null, string token = null)
        {
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {
                if (data != null)
                {
                    var json = JsonSerializer.Serialize(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync(uri, content);
                }
                else
                {
                    return await _httpClient.PostAsync(uri, null);
                }
                
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }

            
        }

        public async Task<HttpResponseMessage> PostFormData(string url, Dictionary<string, string> formData, string token)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")
                );
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {                
                return await _httpClient.PostAsync(uri, new FormUrlEncodedContent(formData));
            }
            catch( HttpRequestException ex )
            {
                Console.WriteLine("ERROR POST FORMDATA REQUEST: {0}", ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> Put(string url, object data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await _httpClient.PutAsync(uri, content);
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine("ERROR PUT REQUEST: {0}", ex.Message);
                throw;
            }
            
        }

        public async Task<HttpResponseMessage> Delete(string url,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl + url));
            try
            {
                return await _httpClient.DeleteAsync(uri);
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine("ERROR DELETE REQUEST: {0}", ex.Message);
                throw;
            }
            
        }
    }
}