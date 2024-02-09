using BoolBnB_MAUI.Services.Interface;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace BoolBnB_MAUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        public const string BaseUrl = "https://052b-79-23-15-49.ngrok-free.app{0}";
        public HttpService() {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json")
               );
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<TResponse> Get<TResponse>(string url)
        {
            var uri = new Uri(string.Format(BaseUrl , url));
            try
            {
                var response = await _httpClient.GetAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return data;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR GET REQUEST: {0}",ex.Message);
                throw;
            }
        }
        public async Task<TResponse> Get<TResponse>(string url, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = new Uri(string.Format(BaseUrl , url));
            try
            {
                var response = await _httpClient.GetAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return data;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR GET REQUEST: {0}",ex.Message);
                throw;
            }
        }

        public async Task<TResponse> Post<TResponse, TRequest>(string url, TRequest data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }
        public async Task<TResponse> Post<TResponse, TRequest>(string url, TRequest data)
        {
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }

        public async Task<TResponse> Post<TResponse>(string url, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var response = await _httpClient.PostAsync(uri,null);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        } 
        public async Task<TResponse> Post<TResponse>(string url)
        {
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var response = await _httpClient.PostAsync(uri,null);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }

        public async Task<TResponse> Patch<TResponse, TRequest>(string url, TRequest data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }

        public async Task<HttpResponseMessage> PostFormData(string url, Dictionary<string, string> formData, string token)
        {
            // check header when make an api call.
            var mdq = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");
            if (_httpClient.DefaultRequestHeaders.Accept.Contains(mdq))
                _httpClient.DefaultRequestHeaders.Accept.Remove(mdq);
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

        public async Task<TResponse> Put<TResponse, TRequest>(string url, TRequest data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }

        public async Task<TResponse> Delete<TResponse>(string url, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri(string.Format(BaseUrl, url));
            try
            {
                var response = await _httpClient.DeleteAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonSerializer.Deserialize<TResponse>(responseContent, _serializerOptions);
                return JsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ERROR POST REQUEST: {0}", ex.Message);
                throw;
            }


        }
    }
}