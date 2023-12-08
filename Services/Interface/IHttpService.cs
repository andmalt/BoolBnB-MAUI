
namespace BoolBnB_MAUI.Services.Interface
{
    public interface IHttpService
    {
        /// <summary>
        /// Make a HTTP GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Get<TResponse>(string url);
        /// <summary>
        /// Make a HTTP GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Get<TResponse>(string url, string token);
        /// <summary>
        /// Make a HTTP POST without data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Post<TResponse>(string url);
        /// <summary>
        /// Make a HTTP POST without data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Post<TResponse>(string url,string token);
        /// <summary>
        /// Make a HTTP POST with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Post<TResponse , TRequest>(string url, TRequest data);
        /// <summary>
        /// Make a HTTP POST with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Post<TResponse , TRequest>(string url, TRequest data, string token );
        /// <summary>
        /// Make a HTTP PUT with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Put<TResponse, TRequest>(string url, TRequest data, string token);
        /// <summary>
        /// Make a HTTP PATCH with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<TResponse> Patch<TResponse, TRequest>(string url, TRequest data, string token);
        /// <summary>
        /// Make a HTTP DELETE call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code</returns>
        Task<TResponse> Delete<TResponse>(string url, string token);
        /// <summary>
        /// Make a HTTP POST with form data body.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> PostFormData(string url, Dictionary<string, string> formData, string token);
    }
}
