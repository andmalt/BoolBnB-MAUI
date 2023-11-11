
namespace BoolBnB_MAUI.Services.Interface
{
    public interface IHttpService
    {
        /// <summary>
        /// Make a HTTP GET call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> Get(string url, string token = null);
        /// <summary>
        /// Make a HTTP POST call with data and not.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> Post(string url, object data = null, string token = null);
        /// <summary>
        /// Make a HTTP POST call with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> Put(string url, object data, string token);
        /// <summary>
        /// Make a HTTP PATCH call with data.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> Patch(string url, object data, string token);
        /// <summary>
        /// Make a HTTP DELETE call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code</returns>
        Task<HttpResponseMessage> Delete(string url, string token);
        /// <summary>
        /// Make a HTTP POST call with form data body.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Return a response message with status code and data.</returns>
        Task<HttpResponseMessage> PostFormData(string url, Dictionary<string, string> formData, string token);
    }
}
